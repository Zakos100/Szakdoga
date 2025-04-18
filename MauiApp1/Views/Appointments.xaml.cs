using MauiApp1.Database;
using MauiApp1.Scheduler;
using MauiApp1.Viewmodels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp1;

public partial class Appointments : ContentPage
{
    public ObservableCollection<ScheduledTaskViewModel> ScheduleResults { get; set; } = new();
    private readonly LocalDBService _localDBService;

    public Appointments()
    {
        InitializeComponent();
        BindingContext = this;

        var dbPath = System.IO.Path.Combine("D:\\egyetem\\VS\\repos\\MauiApp1\\MauiApp1", "WorkersDB");
        _localDBService = new LocalDBService(dbPath);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var tasks = await _localDBService.GetTasksAsync();
        var users = await _localDBService.GetUsersAsync();
        var timeframes = await _localDBService.GetTimeframesAsync();
        var suitabilityList = await _localDBService.GetSuitabilitiesAsync();
        var resources = await _localDBService.GetResourcesAsync();
        var devices = await _localDBService.GetDevicesAsync();
        var userTimeframe = await _localDBService.GetUserTimeframesAsync();
        var scheduler = new SchedulerService(tasks, users, timeframes, suitabilityList, resources, devices, userTimeframe);
        var schedule = scheduler.AssignTasks();

        ScheduleResults.Clear();

        ScheduleResults.Clear();

        var allTasks = schedule
            .SelectMany(entry => entry.Value)
            .OrderBy(t => t.Deadline)
            .ToList();

        ScheduleResults.Add(new ScheduledTaskViewModel
        {
            UserName = "EDD sorrend", // vagy akár "" ha nem kell cím
            AssignedTasks = allTasks.Select(t =>
                $"Feladat sorszáma: {t.TaskID} - Deadline: {t.Deadline:yyyy-MM-dd}").ToList()
        });
    }
}
