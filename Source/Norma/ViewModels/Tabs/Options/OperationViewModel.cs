﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Norma.Eta.Models;
using Norma.Eta.Models.Configurations;
using Norma.Eta.Models.Enums;
using Norma.Eta.Mvvm;
using Norma.Eta.Validations;

using Prism.Commands;

using Reactive.Bindings;

namespace Norma.ViewModels.Tabs.Options
{
    internal class OperationViewModel : ViewModel
    {
        private readonly OperationConfig _operationConfig;
        private readonly RegexValidator _rgxValidator = new RegexValidator();
        private int _editIndex;
        private bool _isEditMode;

        public List<EnumWrap<PostKey>> KeyTypes
            => ((PostKey[]) Enum.GetValues(typeof(PostKey))).Select(w => new EnumWrap<PostKey>(w)).ToList();

        public List<EnumWrap<Branch>> Branches
            => ((Branch[]) Enum.GetValues(typeof(Branch))).Select(w => new EnumWrap<Branch>(w)).ToList();

        public ReactiveProperty<uint> UpdateIntervalOfThumbnails { get; private set; }
        public ReactiveProperty<uint> ReceptionIntervalOfComments { get; private set; }
        public ReactiveProperty<uint> SamplingIntervalOfProgramState { get; private set; }
        public ReactiveProperty<uint> NumberOfHoldingComments { get; private set; }
        public ReactiveProperty<EnumWrap<PostKey>> PostKey { get; private set; }
        public ReactiveProperty<uint> ToastNotificationBeforeMinutes { get; private set; }
        public ReactiveProperty<EnumWrap<Branch>> Branch { get; private set; }
        public ReactiveProperty<bool> IsAbsoluteTime { get; private set; }
        public ReactiveProperty<bool> IsShowFavoriteOnly { get; private set; }
        public ReactiveProperty<uint> Delay { get; private set; }
        public ObservableCollection<MuteKeyword> MuteKeywords { get; }
        public ReactiveProperty<string> Keyword { get; }
        public ReactiveProperty<bool> IsRegex { get; }
        public ReactiveProperty<MuteKeyword> SelectedKeyword { get; }
        public ReactiveProperty<int> SelectedIndex { get; }

        public OperationViewModel(OperationConfig oc)
        {
            _operationConfig = oc;
            UpdateIntervalOfThumbnails = ReactiveProperty.FromObject(oc, w => w.UpdateIntervalOfThumbnails);
            ReceptionIntervalOfComments = ReactiveProperty.FromObject(oc, w => w.ReceptionIntervalOfComments);
            SamplingIntervalOfProgramState = ReactiveProperty.FromObject(oc, w => w.SamplingIntervalOfProgramState);
            NumberOfHoldingComments = ReactiveProperty.FromObject(oc, w => w.NumberOfHoldingComments);
            PostKey = ReactiveProperty.FromObject(oc, w => w.PostKeyType, x => new EnumWrap<PostKey>(x),
                                                  w => w.EnumValue);
            ToastNotificationBeforeMinutes = ReactiveProperty.FromObject(oc, w => w.ToastNotificationBeforeMinutes);
            Branch = ReactiveProperty.FromObject(oc, w => w.Branch, x => new EnumWrap<Branch>(x), w => w.EnumValue);
            IsAbsoluteTime = ReactiveProperty.FromObject(oc, w => w.IsAbsoluteTime);
            IsShowFavoriteOnly = ReactiveProperty.FromObject(oc, w => w.IsShowFavoriteOnly);
            Delay = ReactiveProperty.FromObject(oc, w => w.Delay);
            MuteKeywords = oc.MuteKeywords;
            IsRegex = new ReactiveProperty<bool>(false);
            Keyword = new ReactiveProperty<string>("")
                .SetValidateNotifyError(x => IsRegex.Value ? _rgxValidator.Validate(Keyword.Value) : null);
            Keyword.Subscribe(w => AddMuteKeywordCommand.RaiseCanExecuteChanged()).AddTo(this);
            IsRegex.Subscribe(w =>
            {
                Keyword.ForceValidate();
                AddMuteKeywordCommand.RaiseCanExecuteChanged();
            }).AddTo(this);
            SelectedKeyword = new ReactiveProperty<MuteKeyword>();
            SelectedKeyword.Subscribe(w =>
            {
                EditMuteKeywordCommand.RaiseCanExecuteChanged();
                DeleteMuteKeywordCommand.RaiseCanExecuteChanged();
            }).AddTo(this);
            SelectedIndex = new ReactiveProperty<int>();
        }

        #region AddMuteKeywordCommand

        private DelegateCommand _addMuteKeywordCommand;

        public DelegateCommand AddMuteKeywordCommand =>
            _addMuteKeywordCommand ?? (_addMuteKeywordCommand = new DelegateCommand(AddMuteKeyword, CanAddMuteKeyword));

        private void AddMuteKeyword()
        {
            // なんかｱﾚ
            if (_isEditMode)
            {
                _operationConfig.MuteKeywords.RemoveAt(_editIndex);
                _operationConfig.MuteKeywords.Insert(_editIndex,
                                                     new MuteKeyword(Keyword.Value.Replace("\\n", "\n"), IsRegex.Value));
                _isEditMode = false;
            }
            else
                _operationConfig.MuteKeywords.Add(new MuteKeyword(Keyword.Value, IsRegex.Value));
            Keyword.Value = string.Empty;
            IsRegex.Value = false;
        }

        private bool CanAddMuteKeyword() => !string.IsNullOrWhiteSpace(Keyword.Value) && !Keyword.HasErrors;

        #endregion

        #region EditMuteKeywordCommand

        private DelegateCommand _editMuteKeywordCommand;

        public DelegateCommand EditMuteKeywordCommand =>
            _editMuteKeywordCommand ??
            (_editMuteKeywordCommand = new DelegateCommand(EditMuteKeyword, CanEditMuteKeyword));

        private void EditMuteKeyword()
        {
            Keyword.Value = SelectedKeyword.Value.DisplayKeyword;
            IsRegex.Value = SelectedKeyword.Value.IsRegex;
            _isEditMode = true;
            _editIndex = SelectedIndex.Value;
        }

        private bool CanEditMuteKeyword() => SelectedKeyword.Value != null;

        #endregion

        #region DeleteMuteKeywordCommand

        private DelegateCommand _delMuteKeywordCommand;

        public DelegateCommand DeleteMuteKeywordCommand =>
            _delMuteKeywordCommand ?? (_delMuteKeywordCommand = new DelegateCommand(DelMuteKeyword, CanDelMuteKeyword));

        private void DelMuteKeyword() => _operationConfig.MuteKeywords.RemoveAt(SelectedIndex.Value);

        private bool CanDelMuteKeyword() => SelectedKeyword.Value != null;

        #endregion
    }
}