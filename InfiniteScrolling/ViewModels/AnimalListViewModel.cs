using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace InfiniteScrolling.ViewModels;

[ObservableObject]
public partial class AnimalListViewModel
{
    private readonly AnimalService animalService;
    private List<EntryDetails> allAnimalList = new();
    private int pageSize = 20;
    [ObservableProperty]
    private bool isBusy;
    [ObservableProperty]
    private bool isLoading;

    public ObservableCollection<EntryDetails> AnimalList { get; set; } = new ObservableCollection<EntryDetails>();

    public AnimalListViewModel(AnimalService animalService)
	{
        this.animalService = animalService;
        GetAnimalList();
    }

    void GetAnimalList()
    {
        AnimalList.Clear();
        IsBusy = true;
        Task.Run(async () =>
        {
            allAnimalList = await animalService.GetAnimalList();
            Application.Current.Dispatcher.Dispatch(() =>
            {
                var recordsToBeAdded = allAnimalList.Take(pageSize).ToList();
                foreach(EntryDetails record in recordsToBeAdded)
                {
                    AnimalList.Add(record);
                }
                IsBusy = false;
            });
        });
        
    }

    [RelayCommand]
    public async Task LoadMoreData()
    {
        if(IsLoading) { return; }
        if (allAnimalList.Count > 0)
        {
            IsLoading = true;
            await Task.Delay(1000);
            var recordsToBeAdded = allAnimalList
                .Skip(AnimalList.Count)
                .Take(pageSize)
                .ToList();
            foreach (EntryDetails record in recordsToBeAdded)
            {
                AnimalList.Add(record);
            }
            IsLoading = false;
        }
    }
}
