using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using TIME.Library.Interfaces;
using TIME.UI.Models;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace TIME.UI.ViewModels;

public partial class PageHideFileEditorViewModel : ViewBaseModel
{
    private readonly IFileDialog _fileDialog;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty] private ObservableCollection<FlagItem> _flagItems = new();
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K24MyFactionItems;
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K24MyRiseItems;
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K24ShowcaseItems;
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K24VCItems;
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K25MyFactionItems;
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K25MyRiseItems;
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K25ShowcaseItems;
    [ObservableProperty] private ObservableCollection<FlagItem> _wwe2K25VCItems;
    [ObservableProperty] private int _selectedGameIndex;
    [ObservableProperty] private bool _isWWE2K22Selected;
    [ObservableProperty] private bool _isWWE2K23Selected;
    [ObservableProperty] private bool _isWWE2K24Selected;
    [ObservableProperty] private bool _isWWE2K25Selected;
    [ObservableProperty] private string _filePath = string.Empty;
    //[ObservableProperty] private UserControl _currentGame;
    
    public ICollectionView GroupedFlagItems { get; private set; }

    public PageHideFileEditorViewModel(IFileDialog fileDialog, IServiceProvider serviceProvider)
    {
        _fileDialog = fileDialog;
        _serviceProvider = serviceProvider;

        // Set initial selection to WWE 2K25
        SelectedGameIndex = 2;
        IsWWE2K22Selected = false;
        IsWWE2K23Selected = false;
        IsWWE2K24Selected = true;
        IsWWE2K25Selected = false;

        // Initialize with your flag items
        InitializeFlagItems();

        // Setup grouping
        GroupedFlagItems = CollectionViewSource.GetDefaultView(FlagItems);
        GroupedFlagItems.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
    }

    partial void OnIsWWE2K24SelectedChanged(bool value) => OnPropertyChanged(nameof(WWE2K24MyFactionItems));
    partial void OnIsWWE2K25SelectedChanged(bool value) => OnPropertyChanged(nameof(WWE2K25MyFactionItems));

    partial void OnFlagItemsChanged(ObservableCollection<FlagItem> value)
    {
        OnPropertyChanged(nameof(WWE2K24MyFactionItems));
        OnPropertyChanged(nameof(WWE2K24MyRiseItems));
        OnPropertyChanged(nameof(WWE2K24ShowcaseItems));
        OnPropertyChanged(nameof(WWE2K24VCItems));
        OnPropertyChanged(nameof(WWE2K25MyFactionItems));
        OnPropertyChanged(nameof(WWE2K25MyRiseItems));
        OnPropertyChanged(nameof(WWE2K25ShowcaseItems));
        OnPropertyChanged(nameof(WWE2K25VCItems));
    }

    [RelayCommand]
    public async Task OpenHideFileAsync()
    {
        string filePath = _fileDialog.OpenFile("JSFB FlatBuffer (*.jsfb)|*.jsfb", "Open JSFB File");
        if (!string.IsNullOrEmpty(filePath))
        {
            try
            {
                FilePath = filePath;
                // Add logging
                await ReadFlagsAsync();
            }
            catch (Exception ex)
            {
                // Add logging
            }
        }
    }

    [RelayCommand]
    public async Task ToggleFlagItemAsync(FlagItem flagItem)
    {
        if (flagItem == null) return;
        await SetFlagAsync(flagItem.Position, flagItem.IsChecked ? flagItem.UnlockedValue : flagItem.DefaultValue);
    }

    [RelayCommand]
    public async Task EnableMyFactionContentAsync()
    {
        if (WWE2K24MyFactionItems != null)
        {
            foreach (var item in WWE2K24MyFactionItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }
    }

    [RelayCommand]
    public async Task EnableMyRiseContentAsync()
    {
        if (WWE2K24MyRiseItems != null)
        {
            foreach (var item in WWE2K24MyRiseItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }
    }

    [RelayCommand]
    public async Task EnableShowcaseContentAsync()
    {
        if (WWE2K24ShowcaseItems != null)
        {
            foreach (var item in WWE2K24ShowcaseItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }
    }

    [RelayCommand]
    public async Task EnableVCContentAsync()
    {
        if (WWE2K24VCItems != null)
        {
            foreach (var item in WWE2K24VCItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }
    }

    [RelayCommand]
    public async Task EnableAllContentAsync()
    {
        if (WWE2K24MyFactionItems != null)
        {
            foreach (var item in WWE2K24MyFactionItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }

        if (WWE2K24MyRiseItems != null)
        {
            foreach (var item in WWE2K24MyRiseItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }

        if (WWE2K24ShowcaseItems != null)
        {
            foreach (var item in WWE2K24ShowcaseItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }

        if (WWE2K24VCItems != null)
        {
            foreach (var item in WWE2K24VCItems)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    await SetFlagAsync(item.Position, item.UnlockedValue);
                }
            }
        }
    }

    private async Task SetFlagAsync(int position, byte value)
    {
        try
        {
            if (string.IsNullOrEmpty(FilePath))
                return;
         

            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Write))
            {
                if (position >= stream.Length)
                {
                    // Add logging
                    return;
                }

                stream.Position = position;
                await stream.WriteAsync(new byte[] { value }, 0, 1);

                // For MyFaction category, when setting to 0x02, also set next byte to 0x00
                var flagItem = FlagItems.FirstOrDefault(f => f.Position == position);
                if (flagItem != null && flagItem.Category == "MyFaction" && value == 0x02 && position + 1 < stream.Length)
                {
                    stream.Position = position + 1;
                    await stream.WriteAsync(new byte[] { 0x00 }, 0, 1);
                    
                }
                else
                {
                    // Add logging
                }

            }
        }
        catch (Exception ex)
        {
            // Add logging and error handling
        }
    }

    private async Task ReadFlagsAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                foreach (var flagItem in FlagItems)
                {
                    if (flagItem.Position >= stream.Length)
                    {
                        continue;
                    }

                    stream.Position = flagItem.Position;
                    var buffer = new byte[1];
                    await stream.ReadExactlyAsync(buffer, 0, 1);

                    // Check if the current value matches the unlocked value
                    flagItem.IsChecked = (buffer[0] == flagItem.UnlockedValue);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private async Task ToggleFlagAsync()
    {
        var defaultFlag = FlagItems.FirstOrDefault(f => f.Position == 745838);
        if (defaultFlag != null)
        {
            defaultFlag.IsChecked = !defaultFlag.IsChecked;
            await ToggleFlagItemAsync(defaultFlag);
        }
    }

    // Computed properties for WWE 2K24
    public IEnumerable<FlagItem> WWE2K24MyFactionItems => FlagItems.Where(x => x.Category == "WWE2K24_MyFACTION" && IsWWE2K24Selected);

    public IEnumerable<FlagItem> WWE2K24MyRiseItems => FlagItems.Where(x => x.Category == "WWE2K24_MyRISE" && IsWWE2K24Selected);

    public IEnumerable<FlagItem> WWE2K24ShowcaseItems => FlagItems.Where(x => x.Category == "WWE2K24_SHOWCASE" && IsWWE2K24Selected);

    public IEnumerable<FlagItem> WWE2K24VCItems => FlagItems.Where(x => x.Category == "WWE2K24_VC" && IsWWE2K24Selected);

    // Computed properties for WWE 2K25
    public IEnumerable<FlagItem> WWE2K25MyFactionItems => FlagItems.Where(x => x.Category == "WWE2K25_MyFACTION" && IsWWE2K25Selected);

    public IEnumerable<FlagItem> WWE2K25MyRiseItems => FlagItems.Where(x => x.Category == "WWE2K25_MyRISE" && IsWWE2K25Selected);

    public IEnumerable<FlagItem> WWE2K25ShowcaseItems => FlagItems.Where(x => x.Category == "WWE2K25_SHOWCASE" && IsWWE2K25Selected);

    public IEnumerable<FlagItem> WWE2K25VCItems => FlagItems.Where(x => x.Category == "WWE2K25_VC" && IsWWE2K25Selected);

    partial void OnSelectedGameIndexChanged(int value)
    {
        IsWWE2K22Selected = value == 0;
        IsWWE2K23Selected = value == 1;
        IsWWE2K24Selected = value == 2;
        IsWWE2K25Selected = value == 3;

        // Reinitialize on game change
        InitializeFlagItems();

        // Notify UI that all item collections have changed
        OnPropertyChanged(nameof(WWE2K24MyFactionItems));
        OnPropertyChanged(nameof(WWE2K24MyRiseItems));
        OnPropertyChanged(nameof(WWE2K24ShowcaseItems));
        OnPropertyChanged(nameof(WWE2K24VCItems));
        OnPropertyChanged(nameof(WWE2K25MyFactionItems));
        OnPropertyChanged(nameof(WWE2K25MyRiseItems));
        OnPropertyChanged(nameof(WWE2K25ShowcaseItems));
        OnPropertyChanged(nameof(WWE2K25VCItems));
    }

    private void InitializeFlagItems()
    {
        FlagItems.Clear();
        switch (SelectedGameIndex)
        {
            case 0: // WWE 2K22
                break;
            case 1: // WWE 2K23 
                break;
            case 2: // WWE 2K24
                PopulateWWE2K24FlagItems();
                break;
            case 3: // WWE 2K25
                PopulateWWE2K25FlagItems();
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void PopulateWWE2K25FlagItems()
    {
        FlagItems.Add(new FlagItem("BECKY LYNCH '18", 817366, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("BOOKER T '01", 835686, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("CHAD GABLE '16", 847990, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("CM PUNK (S.E.S.)", 823254, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("CM PUNK '10", 867926, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("CM PUNK '10 (MASKED)", 828854, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("DOMINIK MYSTERIO '23 (MASKED)", 848782, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" BRAY WYATT", 805630, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" CODE RHODES", 755326, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" HULK HOGAN", 807142, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" JOHN CENA", 745838, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" RHEA RIPLEY", 812238, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" THE ROCK", 863454, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" TRISH STRATUS", 854646, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ELITE\" UNDERTAKER [SOON]", 781798, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("\"ICHIBAN\" HULK HOGAN", 868774, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("JEAN-PAUL LEVESQUE", 889286, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("JOHN CENA '10", 748702, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("KELANI JORDAN '23", 791710, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("KING BOOKER", 864278, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("RANDY ORTON '09", 881830, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("ROB VAN DAM '97", 786758, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("ROMAN REIGNS '24", 853262, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("SETH ROLLINS '14", 880318, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("SETH ROLLINS '15", 823662, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("SOLO SIKOA (BLOODLINE SUIT)", 797478, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("SOLO SIKOA (TRIBAL CHIEF)", 805174, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("THE PROTOTYPE", 802142, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("THE ROCK '97", 815302, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("THE ROCK '24", 834222, "WWE2K25_MyFACTION", 2, 5));
        FlagItems.Add(new FlagItem("ULTIMATE WARRIOR (NO PAINT)", 757046, "WWE2K25_MyFACTION", 2, 5));

        FlagItems.Add(new FlagItem("ALUNDRA BLAYZE", 833975, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ASUKA (HAS EXTRA BYTE)", 754639, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("AVA MORENO", 754639, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("BAYLEY", 790503, "WWE2K25_", 2, 4));
        FlagItems.Add(new FlagItem("CHASE", 816743, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CHOSEN", 865263, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("COLE QUINN", 818351, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CM PUNK (HAS EXTRA BYTE)", 871590, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("DDP", 827823, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("DDP '98", 833879, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("\"DEMON\" FINN BALOR", 770327, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("EL MAGO JR", 814383, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("EL ORDINARIO", 799671, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("HECTOR FLORES", 807311, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("JOHN CENA '12", 837775, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("JOSIE JANE", 746839, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("LA CANGREJITA LOCA", 747815, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("THE MANIFESTATION", 734847, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("MEILEE \"FANNY\" FAN", 886327, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("N - MyRISE - Fanny", 8407, "WWE2K25_MyRISE", 2, 5));
        FlagItems.Add(new FlagItem("N - MyRISE - Justine", 8327, "WWE2K25_MyRISE", 2, 5));
        FlagItems.Add(new FlagItem("ODYSSEY RIFT", 756759, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("PARAGON JAY PIERCE", 750359, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("PSYCHO SALLY", 846367, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("R-TRUTH", 800159, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("R-TRUTH (JUDGMENT DAY)", 877143, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("RANDY ORTON '15", 784439, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("RHEA RIPLEY '17", 886967, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("RHEA RIPLEY '20", 797663, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("SCOTT STEINER '03", 831047, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("STARDUST (HAS EXTRA BYTE)", 849398, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("SUPER CENA", 778655, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("\"UNDASHING\" CODY RHODES", 771999, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ZERO (HAS EXTRA BYTE)", 12454, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: ARENA ESTATAL", 3217623, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: JAPAN DOME", 3212639, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: JAPAN HALL", 3212775, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: MOTION CAPTURE STUDIO", 3218407, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: MUTINYMANIA", 3211871, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: NXT ARENA - NO MERCY - MUTINY", 3211143, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: NXT ARENA - MUTINY", 3212015, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: RAW 2011", 3221679, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: RAW MUTINY", 3211279, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: SMACKDOWN MUTINY", 3210759, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: SURVIVOR SERIES - MYRISE", 3211423, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: TBD ARENA", 3204591, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: WCW NWO SOULED OUT 1997", 3226775, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 31 - DAYTIME", 3203087, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 31 - NIGHTTIME", 3203191, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA MYRISE 2K25", 3210903, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ARE: WRESTLING CONVENTION", 3210663, "WWE2K25_MyRISE", 2, 4)); // OK
        FlagItems.Add(new FlagItem("ATT: Bayley '15 Attire", 899, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ATT: Becky Lynch Casual Bundle", 1139, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("N - ATT: Charlotte Flair '14 Attire", 1259, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ATT: Charlotte Flair '19 Attire", 1259, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ATT: Charlotte Flair '17 Attire", 1397, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ATT: Jade Cargill Casual Bundle", 2348, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ATT: Natalya '14 Attire", 2083, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ATT: Nick Aldis Suit Bundle", 2195, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("N - ATT: Seth Rollins Casual Bundle", 2651, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ATT: Triple H Suit Bundle", 2475, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CAS: MEN'S MUTINY MASK", 2401847, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CAS: MEN'S MUTINY SHIRT", 3011614, "WWE2K25_MyRISE", 2, 4)); // HAS 2 BYTES
        FlagItems.Add(new FlagItem("CAS: MEN'S \"THE SHIELD\" TOP", 3011710, "WWE2K25_MyRISE", 2, 4)); // HAS 2 BYTES
        FlagItems.Add(new FlagItem("CAS: WOMEN'S MUTINY MASK", 2369519, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CAS: WOMAN'S nWo SHIRT", 2329207, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CAS: WOMAN'S SAND-WITCH", 2752431, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CAS: WOMAN'S WITCH FLOWERY", 3071751, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CAS: WOMAN'S WITCH TRADITIONAL HAT", 2397567, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("CAS: WOMAN'S WITCH TRADITIONAL JACKET", 2884471, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ENT: BECKY LYNCH '14 IRISH DANCE", 74374, "WWE2K25_MyRISE", 2, 4)); // HAS 2 BYTES
        FlagItems.Add(new FlagItem("ENT: MONSTER TRUCK ENTRANCE", 74470, "WWE2K25_MyRISE", 2, 4)); // HAS 2 BYTES
        FlagItems.Add(new FlagItem("ENT: THRONE SMASH", 74279, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("MOV: Double Team Move - Whirlwind Splash", 1276015, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("TTL: NXT MUTINY CHAMPIONSHIP DESTROYED", 3200438, "WWE2K25_MyRISE", 2, 4)); // HAS 2 BYTES
        FlagItems.Add(new FlagItem("TTL: NXT UNITY CHAMPIONSHIP", 3200535, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("TTL: NXT UNITY TAG CHAMPIONSHIP", 3200743, "WWE2K25_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("PAY: PAPARAZZI", 891579, "WWE2K25_MyRISE", 2, 4));

        FlagItems.Add(new FlagItem("AFA", 777807, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("AFA (MANAGER)", 846071, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARMANDO ALEJANDRO ESTRADA", 804887, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("BAYLEY '20", 749503, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("BECKY LYNCH '17", 769951, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("BIG E", 870087, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("BOBBY \"THE BRAIN \" HEENAN", 868942, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("BOBBY \"THE BRAIN \" HEENAN 2?", 869191, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("CAPTAIN LOU ALBANO", 787351, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("CARMELLA '17", 775199, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("CARMELO HAYES '22", 825295, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("CHARLOTTE FLAIR '17", 861383, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("FATU", 814887, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("GEORGE \"THE ANIMAL\" STEELE", 857927, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("HAKU", 766855, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("\"HIGH CHIEF\" PETER MAIVIA", 758879, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("HULK HOGAN", 783375, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("HUNTER HEARST HELMSLEY", 753087, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("KOFI KINGSTON '17", 771023, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JAMAL", 873975, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JEY USO '10", 821311, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JEY USO '17", 849071, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JIMMY HART", 864799, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JIMMY HART 2?", 864943, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JIMMY USO '10", 838295, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JIMMY USO '17", 829015, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("JOHN CENA '07", 842407, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("LYRA VALKYRIA '24", 851271, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("MR. FUJI", 854767, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("NAOMI '20", 836111, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("RICK STEINER", 854047, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("RIKISHI", 744927, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ROCKY MAIVIA", 861591, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ROMAIN REIGNS '22", 756599, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ROSEY", 868415, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("SCOTT STEINER '93", 848143, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("SAMU", 819095, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("SETH ROLLINS '22", 816383, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("SIKA", 783095, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("SOLO SIKOA '22", 813431, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("\"STONE COLD\" STEVE AUSTIN '00", 781175, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TAMA", 761127, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TAMINA '10", 830567, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TAMINA", 866255, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TRICK WILLIAMS '22", 872391, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("UMAGA", 835847, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("XAVIER WOODS '17", 882527, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("YOKOZUNA", 842623, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: HELL IN A CELL 2017", 3220719, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: KING OF THE RING", 3220967, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: MONEY IN THE BANK 2017", 3221183, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: NEW YEAR'S REVOLUTION 2007", 3221567, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: NO MERCY 2000", 3221447, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: NXT 2.0", 3226935, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: RAW 1997", 3221079, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: ROYAL RUMBLE 2022", 3219295, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: SUPER SHOWDOWN 2020", 3221319, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: THE TRIBAL HALL OF ACKNOWLEDGEMENT", 3220583, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 9", 3220855, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TTL: WWE CHAMPIONSHIP '88 - '98", 3197367, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TTL: WWE CHAMPIONSHIP '05 - '13", 3199327, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TTL: WWE INTERCONTINENTAL CHAMPIONSHIP '94", 3192063, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TTL: WWE SMACKDOWN TAG TEAM CHAMPIONSHIP", 3195951, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TTL: WWE SMACKDOWN WOMEN'S CHAMPIONSHIP", 3193055, "WWE2K25_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("TTL: WWE UNIVERSAL CHAMPIONSHIP", 3192751, "WWE2K25_SHOWCASE", 2, 3));

        FlagItems.Add(new FlagItem("ALEXA BLISS", 802775, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("BARON CORBIN", 824415, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("NEW WAVE PACK - ALEX SHELLEY", 786302, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("NEW WAVE PACK - CHRIS SABIN", 791566, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("NEW WAVE PACK - GULIA", 759094, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("NEW WAVE PACK - STEPHANIE VAQUER", 838934, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("NEW WAVE PACK - TRAVIS SCOTT", 818886, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2176999, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2184239, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2370407, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2370831, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2390303, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2391015, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2398799, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2398983, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2399343, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2572391, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 2883423, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("CAS MASK - UNDERTAKER '95", 2899911, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 3015655, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("FUTURE DLC ITEM", 3017647, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("LILLY", 869311, "WWE2K25_DLC", 2, 10));
        FlagItems.Add(new FlagItem("NIKKI CROSS '23", 753479, "WWE2K25_DLC", 2, 10));

        FlagItems.Add(new FlagItem("WCW HARDCORE CHAMPIONSHIP", 3193779, "WWE2K25_VC", 2, 1));
        FlagItems.Add(new FlagItem("WCW WORLD TAG TEAM CHAMPIONSHIP", 3193675, "WWE2K25_VC", 2, 1));
    }

    private void PopulateWWE2K24FlagItems()
    {
        FlagItems.Clear();  // Make sure we start fresh

        // Add MyFACTION items, some will have 2 bytes and not 1.

        FlagItems.Add(new FlagItem("ASUKA (DM)", 431790, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("BECKY LYNCH '18", 429798, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("BIANCA BELAIR (DM)", 417618, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("BIG E", 418270, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"BIG POPPA PUMP\" SCOTT STEINER", 470726, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("BOOKER T '01", 455986, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("BRON BREAKKER '23", 462722, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("CHAD GABLE '16", 435190, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("CM PUNK '10", 458306, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("CM PUNK '10 (MASKED)", 443974, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("CM PUNK (PIPER)", 472794, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("CM PUNK (S.E.S.)", 471562, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("DAMIAN PRIEST", 420802, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("DOMINIK MYSTERIO (MASKED)", 433466, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"ELITE\" HULK HOGAN", 450522, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"ELITE\" JOHN CENA", 417166, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"ELITE\" THE ROCK", 443750, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"ELITE\" ROMAN REIGNS", 416066, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"ELITE\" TIFFANY STRATTON", 480942, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"ICHIBAN\" HULK HOGAN", 469522, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("HOLLYWOOD HOGAN (WOLFPAC)", 416790, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("THE HURRICANE", 420910, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("JAUN CENA", 429506, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("KEVIN NASH (WOLFPAC)", 432598, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("KEVIN OWENS (STONE COLD)", 454906, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("KING NAKAMURA", 418150, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("LEX LUGER (WOLFPAC)", 421878, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("LIV MORGAN '22", 456778, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"MACHO MAN\" RANDY SAVAGE (WOLFPAC)", 435430, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("MANKIND '96", 430986, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("MICHIN (DM)", 437378, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("MR. PERFECT (WOLFPAC)", 482578, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("RANDY ORTON '09", 428870, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("RAQUEL RODRIGUEZ (DM)", 425542, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"RAVISHING\" RICK RUDE (WOLFPAC)", 466078, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("RHEA RIPLEY (CROWN JEWEL)", 474810, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("RHEA RIPLEY (HHH)", 472094, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("ROMAN REIGNS '24", 479654, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("ROMAN REIGNS (DM)", 477078, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("SCOTT HALL (WOLFPAC)", 463070, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("SCOTT STEINER (WOLFPAC)", 476614, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("SETH ROLLINS '14", 450086, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("SETH ROLLINS '15", 419670, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("SHAWN MICHAELS (HOGAN)", 447686, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("SHEAMUS '09", 431114, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("\"SLIM JIM\" MACHO MAN RANDY SAVAGE", 442978, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("THE ROCK '24", 413046, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("TRICK WILLIAMS '22", 421970, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("TRIPLE H (KING OF KINGS)", 414950, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("UNDERTAKER (HBK)", 427450, "WWE2K24_MyFACTION", 2, 9));
        FlagItems.Add(new FlagItem("XAVIER WOODS (DM)", 484946, "WWE2K24_MyFACTION", 2, 9));

        // Add MyRise items, some will have 2 bytes and not 1.

        FlagItems.Add(new FlagItem("ARE: CAPTIVE AUDIENCE TALK SHOW", 1360683, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: ARENA ESTATAL.", 1361314, "WWE2K24_MyRISE", 2, 4)); // 2 BYTES
        FlagItems.Add(new FlagItem("ARE: CLUB U.K.", 1366490, "WWE2K24_MyRISE", 2, 4)); // 2 BYTES
        FlagItems.Add(new FlagItem("ARE: DOWN-UP-DOWN-UP ARENA", 1366490, "WWE2K24_MyRISE", 2, 4)); // 2 BYTES
        FlagItems.Add(new FlagItem("ARE: GM OFFICE - THE MIZ", 1362927, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: INTERVIEW SET - RAW", 3164739, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: LOCKER ROOM - SMACKDOWN", 1363891, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: LOCKER ROOM - WOMEN'S - RAW", 1361407, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: JAPAN DOME", 1362287, "WWE2K24_MyRISE", 2, 4)); // 2 BYTES
        FlagItems.Add(new FlagItem("ARE: JAPAN HALL - BACKSTAGE", 1362287, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: JOSHI JAPAN", 1364274, "WWE2K24_MyRISE", 2, 4)); // 2 BYTES
        FlagItems.Add(new FlagItem("ARE: LAW - BACKSTAGE", 1360439, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: MOTION CAPTURE STUDIO", 1363962, "WWE2K24_MyRISE", 2, 4)); // 2 BYTES
        FlagItems.Add(new FlagItem("ARE: MOVIE SET", 1367215, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: PERFORMANCE CENTER - WEIGHT ROOM", 1367395, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: SMACKDOWN", 1354323, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: SUMMERSLAM - RED CARPET", 1367867, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: SUPERNATURAL LAIR", 1361847, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: TDB ARENA (EMPTY)", 833975, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: TDB BACKSTAGE", 1360751, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: TRAINER'S ROOM", 1365747, "WWE2K24_MyRISE", 2, 4));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA - MYRISE", 1361914, "WWE2K24_MyRISE", 2, 4)); // 2 BYTES

        // Add Showcase Items
        
        FlagItems.Add(new FlagItem("CHR: BRAY WYATT (SNME)", 415750, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("CHR: JOHN CENA '20", 460554, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("CHR: \"STONE COLD\" STEVE AUSTIN", 460358, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("CHR: ULTIMATE WARRIOR", 431490, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 5", 1358882, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 6", 1363578, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 8", 1360098, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 10", 1364514, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 17", 1358962, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 20", 1365954, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 25", 1354890, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 30", 1363658, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 31 - NIGHTTIME", 1362090, "WWE2K24_SHOWCASE", 2, 3));
        FlagItems.Add(new FlagItem("ARE: WRESTLEMANIA 36", 1364418, "WWE2K24_SHOWCASE", 2, 3));

        // Add VC Items

        FlagItems.Add(new FlagItem("ARE: ECW ONE NIGHT STAND 2006", 1369822, "WWE2K24_VC", 2, 1));
        FlagItems.Add(new FlagItem("ARE: RAW 2002", 1362446, "WWE2K24_VC", 2, 1));
        FlagItems.Add(new FlagItem("ARE: SMACKDOWN 2002", 1354798, "WWE2K24_VC", 2, 1));
        FlagItems.Add(new FlagItem("ARE: SUMMERSLAM 1998", 1361518, "WWE2K24_VC", 2, 1));
        FlagItems.Add(new FlagItem("TTL: ECW CHAMPIONSHIP '08-'10", 1347982, "WWE2K24_VC", 2, 1));

        System.Diagnostics.Debug.WriteLine($"WWE2K24 Items populated: {FlagItems.Count}");
        System.Diagnostics.Debug.WriteLine($"WWE2K24 MyFaction Items: {FlagItems.Where(x => x.Category == "WWE2K24_MyFACTION").Count()}");
    }
}
