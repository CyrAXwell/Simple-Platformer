using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class PlayerData
{
    private TrailSkins _selectedTrailSkin;
    private OrbSkins _selectedOrbSkin;
    private List<TrailSkins> _openTrailSkins;
    private List<OrbSkins> _openOrbSkins;
    private int _money;
    private int _maxMoney;
    private int _unlockedLevels;
    private List<string> _levelsStars;
    private List<bool> _achievements;
    private int _completedLevels;

    public PlayerData()
    {
        _money = 0;
        _maxMoney = 0;

        _selectedTrailSkin = TrailSkins.DefaultWhite;
        _selectedOrbSkin = OrbSkins.DefaultWhite;

        _openTrailSkins = new List<TrailSkins>() { _selectedTrailSkin };
        _openOrbSkins = new List<OrbSkins>() { _selectedOrbSkin };

        _unlockedLevels = 1;
        _levelsStars = new List<string>();

        for(int i = 0; i < _unlockedLevels; i++)
            _levelsStars.Add(null);
        
        _achievements = new List<bool>();
        _completedLevels = 0;

    }

    [JsonConstructor]
    public PlayerData(int money, TrailSkins selectedTrailSkin, OrbSkins selectedOrbSkin, List<TrailSkins> openTrailSkins, List<OrbSkins> openOrbSkins, int unlockedLevels, List<string> levelsStars, List<bool> achievements, int completedLevels, int maxMoney)
    {
        Money = money;
        MaxMoney = maxMoney;
        _selectedTrailSkin = selectedTrailSkin;
        _selectedOrbSkin = selectedOrbSkin;

        _openTrailSkins = new List<TrailSkins>(openTrailSkins);
        _openOrbSkins = new List<OrbSkins>(openOrbSkins);

        _unlockedLevels = unlockedLevels;
        _levelsStars = new List<string>(levelsStars);

        _achievements = new List<bool>(achievements);
        _completedLevels = completedLevels;
    }

    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));

            _money = value;
        }
    }

    public int MaxMoney
    {
        get => _maxMoney;

        set
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));

            _maxMoney = value;
        }
    }

    public TrailSkins SelectedTrailSkin
    {
        get => _selectedTrailSkin;

        set
        {
            if(_openTrailSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedTrailSkin = value;
        }
    }

    public OrbSkins SelectedOrbSkin
    {
        get => _selectedOrbSkin;

        set
        {
            if(_openOrbSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedOrbSkin = value;
        }
    }

    public int UnlockedLevels
    {
        get => _unlockedLevels;

        set 
        {
            if (value < 1)
                throw new ArgumentException(nameof(value));
            
            _unlockedLevels = value;
        }
    }

    public int CompletedLevels
    {
        get => _completedLevels;

        set 
        {
            if (value < 1)
                throw new ArgumentException(nameof(value));
            
            _completedLevels = value;
        }
    }

    public IEnumerable<TrailSkins> OpenTrailSkins => _openTrailSkins;

    public IEnumerable<OrbSkins> OpenOrbSkins => _openOrbSkins;

    public IEnumerable<string> LevelsStars => _levelsStars;

    public IEnumerable<bool> Achievements => _achievements;

    public void OpenTrailSkin(TrailSkins skin)
    {
        if(_openTrailSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openTrailSkins.Add(skin);
    }

    public void OpenOrbSkin(OrbSkins skin)
    {
        if(_openOrbSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openOrbSkins.Add(skin);
    }

    public void AddLevelsStars(int level, string stars)
    {
        if (level < 1)
                throw new ArgumentException(nameof(level));

        if (_levelsStars.Count < level)
            _levelsStars.Add(stars);
        else
            _levelsStars[level-1] = stars;
    }

    public int GetStarsAmount()
    {
        int value = 0;
        
        foreach (string level in _levelsStars)
        {
            if (level != null)
                value += level.Count();
        }
        return value;
    }

    public int GetFullLevelCompletedAmount()
    {
        int value = 0;
        
        foreach (string level in _levelsStars)
        {
            if (level != null && level.Count() == 3)
                value ++;
        }
        return value;
    }

    public void CreateAchivement(int id)
    {
        if (id >= _achievements.Count())
            _achievements.Add(false); 
    }

    public void CompleteAchievement(int id)
    {
        _achievements[id] = true;
    }

    public bool GetAchievementState(int id)
    {
        if (id >= _achievements.Count())
            throw new ArgumentException(nameof(id));

        return _achievements[id];
    }

}
