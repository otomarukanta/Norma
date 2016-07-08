﻿using System;
using System.Diagnostics;

using Norma.Eta.Properties;

namespace Norma.Eta.Models
{
    [Obsolete("Removed by 1.5", false)]
    public enum AbemaChannel
    {
        /// <summary>
        ///     1ch - Abema news/
        /// </summary>
        AbemaNews,

        /// <summary>
        ///     2ch - Abema SPECIAL
        /// </summary>
        AbemaSpecial,

        /// <summary>
        ///     3ch - SPECIAL PLUS
        /// </summary>
        SpecialPlus,

        /// <summary>
        ///     Nch - Onegai Ranking
        /// </summary>
        OnegaiRanking,

        /// <summary>
        ///     Nch - SPECIAL PLUS 2
        /// </summary>
        SpecialPlus2,

        /// <summary>
        ///     8ch - ドラマ CHANNEL
        /// </summary>
        DramaChannel,

        /// <summary>
        ///     9ch - REALITY SHOW
        /// </summary>
        RealityShow,

        /// <summary>
        ///     10ch - MTV HITS
        /// </summary>
        MtvHits,

        /// <summary>
        ///     11ch - SPACE SHOWER MUSIC
        /// </summary>
        SpaceShowerMusic,

        /// <summary>
        ///     12ch - Documentary
        /// </summary>
        Documentary,

        /// <summary>
        ///     13ch - バラエティ CHANNEL
        /// </summary>
        VarietyChannel,

        /// <summary>
        ///     14ch - ペット
        /// </summary>
        Pet,

        /// <summary>
        ///     15ch - CLUB CHANNEL
        /// </summary>
        ClubChannel,

        /// <summary>
        ///     16ch - WORLD SPORTS
        /// </summary>
        WorldSports,

        /// <summary>
        ///     17ch - EDGE SPORT HD
        /// </summary>
        EdgeSportHd,

        /// <summary>
        ///     18ch - VICE
        /// </summary>
        Vice,

        /// <summary>
        ///     19ch - アニメ24
        /// </summary>
        Anime24,

        /// <summary>
        ///     20ch - 深夜アニメ
        /// </summary>
        MidnightAnime,

        /// <summary>
        ///     21ch - なつかしアニメ
        /// </summary>
        OldtimeAnime,

        /// <summary>
        ///     22ch - 家族アニメ
        /// </summary>
        FamilyAnime,

        /// <summary>
        ///     23ch - 新作アニメ
        /// </summary>
        NewAnime,

        /// <summary>
        ///     24ch - ヨコノリ Surf Snow Skate
        /// </summary>
        YokonoriSports,

        /// <summary>
        ///     25ch - 釣り
        /// </summary>
        Fishing,

        /// <summary>
        ///     26ch - 麻雀
        /// </summary>
        Mahjong,

        SportsLive
    }

    public static class AbemaChannelExt
    {
        public static string ToUrlString(this AbemaChannel channel)
        {
            switch (channel)
            {
                case AbemaChannel.Documentary:
                case AbemaChannel.Pet:
                case AbemaChannel.Vice:
                case AbemaChannel.Anime24:
                case AbemaChannel.Fishing:
                case AbemaChannel.Mahjong:
                    return channel.ToString().ToLower();

                case AbemaChannel.OnegaiRanking:
                    return "onegai-ranking";

                case AbemaChannel.AbemaNews:
                    return "abema-news";

                case AbemaChannel.AbemaSpecial:
                    return "abema-special";

                case AbemaChannel.SpecialPlus:
                    return "special-plus";

                case AbemaChannel.SpecialPlus2:
                    return "special-plus-2";

                case AbemaChannel.RealityShow:
                    return "reality-show";

                case AbemaChannel.MtvHits:
                    return "mtv-hits";

                case AbemaChannel.SpaceShowerMusic:
                    return "space-shower";

                case AbemaChannel.DramaChannel:
                    return "drama";

                case AbemaChannel.VarietyChannel:
                    return "variety";

                case AbemaChannel.ClubChannel:
                    return "club-channel";

                case AbemaChannel.WorldSports:
                    return "world-sports";

                case AbemaChannel.YokonoriSports:
                    return "yokonori-sports";

                case AbemaChannel.MidnightAnime:
                    return "midnight-anime";

                case AbemaChannel.OldtimeAnime:
                    return "oldtime-anime";

                case AbemaChannel.FamilyAnime:
                    return "family-anime";

                case AbemaChannel.NewAnime:
                    return "new-anime";

                case AbemaChannel.EdgeSportHd:
                    return "edge-sport";

                case AbemaChannel.SportsLive:
                    return "world-sports-1";

                // case AbemaChannels.AbemaTvFresh:
                //    return "abematv-fresh";

                default:
                    throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
            }
        }

        public static string ToIdentifier(string url)
        {
            return url.Replace("https://abema.tv/now-on-air/", "");
        }

        public static AbemaChannel FromUrlString(string url)
        {
            var str = url.Replace("https://abema.tv/now-on-air/", "");
            switch (str)
            {
                case "onegai-ranking":
                    return AbemaChannel.OnegaiRanking;

                case "abema-news":
                    return AbemaChannel.AbemaNews;

                case "abema-special":
                    return AbemaChannel.AbemaSpecial;

                case "special-plus":
                    return AbemaChannel.SpecialPlus;

                case "special-plus-2":
                    return AbemaChannel.SpecialPlus2;

                case "reality-show":
                    return AbemaChannel.RealityShow;

                case "mtv-hits":
                    return AbemaChannel.MtvHits;

                case "space-shower":
                    return AbemaChannel.SpaceShowerMusic;

                case "drama":
                    return AbemaChannel.DramaChannel;

                case "documentary":
                    return AbemaChannel.Documentary;

                case "variety":
                    return AbemaChannel.VarietyChannel;

                case "pet":
                    return AbemaChannel.Pet;

                case "club-channel":
                    return AbemaChannel.ClubChannel;

                case "world-sports":
                    return AbemaChannel.WorldSports;

                case "yokonori-sports":
                    return AbemaChannel.YokonoriSports;

                case "vice":
                    return AbemaChannel.Vice;

                case "anime24":
                    return AbemaChannel.Anime24;

                case "midnight-anime":
                    return AbemaChannel.MidnightAnime;

                case "oldtime-anime":
                    return AbemaChannel.OldtimeAnime;

                case "family-anime":
                    return AbemaChannel.FamilyAnime;

                case "new-anime":
                    return AbemaChannel.NewAnime;

                case "edge-sport":
                    return AbemaChannel.EdgeSportHd;

                case "fishing":
                    return AbemaChannel.Fishing;

                case "mahjong":
                    return AbemaChannel.Mahjong;

                case "world-sports-1":
                    return AbemaChannel.SportsLive;

                default:
                    throw new ArgumentOutOfRangeException(nameof(url), url, null);
            }
        }

        public static string ToLocaleString(this AbemaChannel obj)
        {
            return ToLocaleString(obj.ToUrlString());
        }

        public static string ToLocaleString(string identifier)
        {
            identifier = identifier.Replace("-", "_");
            try
            {
                return (string) typeof(Resources).GetProperty(identifier).GetValue(null);
            }
            catch
            {
                Debug.WriteLine($"WARN: i18n resource key '{identifier}' is not found on resx.");
            }
        }
    }
}