using Memory;
using System.ComponentModel;
using System.Windows;

namespace MHFZ_Overlay.addresses
{
    public abstract class AddressModel : INotifyPropertyChanged
    {
        public readonly Mem M;

        public int SavedMonster1MaxHP = 0;
        public int SavedMonster2MaxHP = 0;
        private int SavedMonster3MaxHP = 0;
        private int SavedMonster4MaxHP = 0;


        private int SavedMonster1ID = 0;
        private int SavedMonster2ID = 0;

        public AddressModel(Mem m) => M = m;

        public int SelectedMonster = 0;

        public bool ShowMonsterInfos { get; set; } = true;

        public bool ShowMonsterHPBars { get; set; } = true;

        abstract public bool IsNotRoad();

        abstract public int HitCountInt();
        abstract public int DamageDealt();

        abstract public int TimeInt();
        abstract public int TimeDefInt();

        abstract public int WeaponRaw();
        abstract public int WeaponType();

        abstract public int LargeMonster1ID();
        abstract public int LargeMonster2ID();
        abstract public int LargeMonster3ID();
        abstract public int LargeMonster4ID();

        abstract public int Monster1HPInt();
        abstract public int Monster2HPInt();
        abstract public int Monster3HPInt();
        abstract public int Monster4HPInt();

        abstract public string Monster1AtkMult();
        abstract public string Monster2AtkMult();

        abstract public string Monster1DefMult();
        abstract public string Monster2DefMult();

        abstract public int Monster1Poison();
        abstract public int Monster1PoisonNeed();
        abstract public int Monster1Sleep();
        abstract public int Monster1SleepNeed();
        abstract public int Monster1Para();
        abstract public int Monster1ParaNeed();
        abstract public int Monster1Blast();
        abstract public int Monster1BlastNeed();
        abstract public int Monster1Stun();
        abstract public int Monster1StunNeed();
        abstract public string Monster1Size();

        abstract public int Monster2Poison();
        abstract public int Monster2PoisonNeed();
        abstract public int Monster2Sleep();
        abstract public int Monster2SleepNeed();
        abstract public int Monster2Para();
        abstract public int Monster2ParaNeed();
        abstract public int Monster2Blast();
        abstract public int Monster2BlastNeed();
        abstract public int Monster2Stun();
        abstract public int Monster2StunNeed();
        abstract public string Monster2Size();

        abstract public int RoadSelectedMonster();
        public bool HasMonster1 => ShowHPBar(LargeMonster1ID(), Monster1HPInt());
        public bool HasMonster2 => ((LargeMonster2ID() > 0 && Monster2HPInt() != 0 && GetNotRoad()) || Configuring); // road check since the 2nd choice is used as the monster #1
        public bool HasMonster3 => ShowHPBar(LargeMonster3ID(), Monster3HPInt());
        public bool HasMonster4 => ShowHPBar(LargeMonster4ID(), Monster4HPInt());

        public int HitCount => HitCountInt();

        public bool _configuring = false;

        public bool Configuring { get { return _configuring; } set { _configuring = value; ReloadData(); } }

        public bool ShowHPBar(int monsterId, int monsterHp)
        {
            return (monsterId > 0 && monsterHp != 0) || Configuring;
        }


        public bool? roadOverride()
        {
            Settings s = (Settings)Application.Current.TryFindResource("Settings");
            if (s.IsRoadOverride == 1)
                return true;
            else if (s.IsRoadOverride == 2)
                return false;
            return null;
        }

        public bool GetNotRoad()
        {
            bool? b = roadOverride();
            if (b != null)
                return b.Value;
            return IsNotRoad();
        }

        public string Time
        {
            get
            {
                int time = TimeInt();
                int timeS = time / 30;
                return string.Format("{0:D2}:{1:D2}", timeS / 60, timeS % 60);
            }
        }
        public string ATK
        {
            get
            {
                int weaponRaw = WeaponRaw();
                int weaponType = WeaponType();
                return weaponRaw.ToString();// ((int)(GetMultFromWeaponType(weaponType) * weaponRaw)).ToString();
            }
        }
        public string Size
        {
            get
            {
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1Size();
                    case 1:
                        return Monster2Size();
                    default:
                        return Monster1Size();
                }
            }
        }


        public string AtkMult
        {
            get
            {
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1AtkMult();
                    case 1:
                        return Monster2AtkMult();
                    default:
                        return Monster1AtkMult();
                }
            }
        }

        public string DefMult
        {
            get
            {
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1DefMult();
                    case 1:
                        return Monster2DefMult();
                    default:
                        return Monster1DefMult();
                }
            }
        }

        public int PoisonCurrent
        {
            get
            {
                if (Configuring)
                    return 50;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1Poison();
                    case 1:
                        return Monster2Poison();
                    default:
                        return Monster1Poison();
                }
            }
        }

        public int PoisonMax
        {
            get
            {
                if (Configuring)
                    return 100;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1PoisonNeed();
                    case 1:
                        return Monster2PoisonNeed();
                    default:
                        return Monster1PoisonNeed();
                }
            }
        }

        public int SleepCurrent
        {
            get
            {
                if (Configuring)
                    return 50;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1Sleep();
                    case 1:
                        return Monster2Sleep();
                    default:
                        return Monster1Sleep();
                }
            }
        }

        public int SleepMax
        {
            get
            {
                if (Configuring)
                    return 100;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1SleepNeed();
                    case 1:
                        return Monster2SleepNeed();
                    default:
                        return Monster1SleepNeed();
                }
            }
        }
        public int ParaCurrent
        {
            get
            {
                if (Configuring)
                    return 50;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1Para();
                    case 1:
                        return Monster2Para();
                    default:
                        return Monster1Para();
                }
            }
        }

        public int ParaMax
        {
            get
            {
                if (Configuring)
                    return 100;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1ParaNeed();
                    case 1:
                        return Monster2ParaNeed();
                    default:
                        return Monster1ParaNeed();
                }
            }
        }



        public int BlastCurrent
        {
            get
            {
                if (Configuring)
                    return 50;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1Blast();
                    case 1:
                        return Monster2Blast();
                    default:
                        return Monster1Blast();
                }
            }
        }

        public int BlastMax
        {
            get
            {
                if (Configuring)
                    return 100;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1BlastNeed();
                    case 1:
                        return Monster2BlastNeed();
                    default:
                        return Monster1BlastNeed();
                }
            }
        }
        public int StunCurrent
        {
            get
            {
                if (Configuring)
                    return 50;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1Stun();
                    case 1:
                        return Monster2Stun();
                    default:
                        return Monster1Stun();
                }
            }
        }

        public int StunMax
        {
            get
            {
                if (Configuring)
                    return 100;
                switch (SelectedMonster)
                {
                    case 0:
                        return Monster1StunNeed();
                    case 1:
                        return Monster2StunNeed();
                    default:
                        return Monster1StunNeed();
                }
            }
        }

        public string Monster1Name => getMonsterName(GetNotRoad() || RoadSelectedMonster() == 0 ? LargeMonster1ID() : LargeMonster2ID()); //monster 1 is used for the first display and road uses 2nd choice to store 2nd monster
        public string Monster2Name => getMonsterName(LargeMonster2ID());
        public string Monster3Name => getMonsterName(LargeMonster3ID());
        public string Monster4Name => getMonsterName(LargeMonster4ID());

        public string getMonsterName(int id)
        {
            if (Configuring)
                return "Monster Name";
            if (id == 0)
                return "";
            Dictionary.List.MonsterID.TryGetValue(id, out string? monstername);
            return monstername + ":";
        }

        public string Monster1HP => !Configuring ? Monster1HPInt().ToString() : "50";

        public string Monster1MaxHP
        {
            get
            {
                if (Configuring)
                    return "100";
                if (TimeDefInt() == TimeInt())
                    SavedMonster1MaxHP = Monster1HPInt();
                if (LargeMonster1ID() > 0 && SavedMonster1ID == 0)
                {
                    SavedMonster1MaxHP = Monster1HPInt();
                    SavedMonster1ID = LargeMonster1ID();
                }
                if (SavedMonster1ID > 0)
                    SavedMonster1ID = LargeMonster1ID();

                if (GetNotRoad() || RoadSelectedMonster() == 0)
                {
                    return SavedMonster1MaxHP.ToString();
                }
                else
                {
                    return Monster2MaxHP;
                }
            }
        }
        public string Monster2HP => !Configuring ? Monster2HPInt().ToString() : "50";

        public string Monster2MaxHP
        {
            get
            {
                if (Configuring)
                    return "100";
                if (TimeDefInt() == TimeInt())
                    SavedMonster2MaxHP = Monster2HPInt();
                if (RoadSelectedMonster() > 0 && SavedMonster2ID == 0)
                {
                    SavedMonster2MaxHP = Monster2HPInt();
                    SavedMonster2ID = RoadSelectedMonster();
                }
                if (SavedMonster2ID > 0)
                    SavedMonster2ID = RoadSelectedMonster();

                return SavedMonster2MaxHP.ToString();
            }
        }
        public string Monster3HP => !Configuring ? Monster3HPInt().ToString() : "50";

        public string Monster3MaxHP
        {
            get
            {
                if (Configuring)
                    return "100";
                if (TimeDefInt() == TimeInt())
                    SavedMonster3MaxHP = Monster3HPInt();
                return SavedMonster3MaxHP.ToString();
            }
        }
        public string Monster4HP => !Configuring ? Monster4HPInt().ToString() : "50";

        public string Monster4MaxHP
        {
            get
            {
                if (Configuring)
                    return "100";
                if (TimeDefInt() == TimeInt())
                    SavedMonster4MaxHP = Monster4HPInt();
                return SavedMonster4MaxHP.ToString();
            }
        }

        public float GetMultFromWeaponType(int weaponType)
        {
            switch (weaponType)
            {
                case 0:
                case 1:
                    return 1.4f;
                case 2:
                case 3:
                    return 4.8f;
                case 4:
                case 5:
                    return 5.2f;
                case 6:
                case 7:
                    return 2.3f;
                case 8:
                case 9:
                case 10:
                case 22:
                    return 1.2f;
                case 34:
                case 36:
                    return 5.4f;
                case 35:
                    return 1.8f;
                default:
                    return 1f;
            }
        }





        public event PropertyChangedEventHandler? PropertyChanged;

        public void ReloadData()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
