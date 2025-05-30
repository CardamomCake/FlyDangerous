﻿using System.Collections.Generic;
using JetBrains.Annotations;
using Misc;

namespace Audio {
    public class MusicTrack : IFdEnum {
        private static int _id;

        // Alphabetical order please (except None)!
        public static readonly MusicTrack None = new("None", "", "");

        public static readonly MusicTrack BeautifulCatastrophe =
            new("Beautiful Catastrophe", "Ben Fox", "Levels/beautiful_catastrophe_loop", "Levels/beautiful_catastrophe_intro");

        public static readonly MusicTrack ChaosAtTheSpaceship =
            new("Chaos At The Spaceship", "Out of Flux", "Levels/chaos_at_the_spaceship_loop", "Levels/chaos_at_the_spaceship_intro");

        public static readonly MusicTrack DigitalBattleground =
            new("Digital Battleground", "Night Rider 87", "Levels/digital_battleground_loop", "Levels/digital_battleground_intro");

        public static readonly MusicTrack GalacticRealms = new("Galactic Realms", "Night Rider 87", "Levels/galactic_realms_loop",
            "Levels/galactic_realms_intro");

        public static readonly MusicTrack Hooligans = new("Hooligans", "Evgeny Bardyuzha", "Levels/hooligans_loop", "Levels/hooligans_intro");
        public static readonly MusicTrack Hydra = new("Hydra", "Kryptos", "Levels/hydra_loop", "Levels/hydra_intro");
        public static readonly MusicTrack Juno = new("Juno", "OTNO", "Levels/juno_loop", "Levels/juno_intro");
        public static readonly MusicTrack MuscleCar = new("Muscle Car", "Evgeny Bardyuzha", "Levels/muscle_car_loop", "Levels/muscle_car_intro");
        public static readonly MusicTrack NoMoreTime = new("No More Time", "RocknStock", "Levels/no_more_time_loop", "Levels/no_more_time_intro");
        public static readonly MusicTrack PowerPunch = new("Power Punch", "2050", "Levels/power_punch_loop", "Levels/power_punch_intro");
        public static readonly MusicTrack PUNKD = new("PUNK'D", "Out of Flux", "Levels/punkd_loop", "Levels/punkd_intro");
        public static readonly MusicTrack RockYourBody = new("Rock Your Body", "FASSounds", "Main Menu/rock_your_body_loop", "Main Menu/rock_your_body_intro");
        public static readonly MusicTrack Spearhead = new("Spearhead", "Evgeny Bardyuzha", "Levels/spearhead_loop", "Levels/spearhead_intro");


        private MusicTrack(string name, string artist, string musicTrackToLoad, string introTrackToLoad = null) {
            Id = GenerateId;
            Name = name;
            Artist = artist;
            IntroTrackToLoad = introTrackToLoad;
            MusicTrackToLoad = musicTrackToLoad;
        }

        private static int GenerateId => _id++;
        public string MusicTrackToLoad { get; }
        [CanBeNull] public string IntroTrackToLoad { get; }
        public bool HasIntro => IntroTrackToLoad != null;

        public int Id { get; }
        public string Name { get; }
        public string Artist { get; }

        public static IEnumerable<MusicTrack> List() {
            return new[] {
                None,
                BeautifulCatastrophe,
                ChaosAtTheSpaceship,
                DigitalBattleground,
                GalacticRealms,
                Hooligans,
                Hydra,
                Juno,
                MuscleCar,
                NoMoreTime,
                PowerPunch,
                PUNKD,
                RockYourBody,
                Spearhead
            };
        }

        public static MusicTrack FromString(string locationString) {
            return FdEnum.FromString(List(), locationString);
        }

        public static MusicTrack FromId(int id) {
            return FdEnum.FromId(List(), id);
        }
    }
}