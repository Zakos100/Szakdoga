using MauiApp1.Database;
using MauiApp1.Scheduler;
using MauiApp1.Services;
using MauiApp1.Viewmodels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.System;

namespace MauiApp1;

public partial class Appointments : ContentPage
{
    public ObservableCollection<ScheduledTaskViewModel> ScheduleResults { get; set; } = new();
    public ObservableCollection<ScheduledTaskViewModel> TabuSearchResults { get; set; } = new();

    public ObservableCollection<LatenessViewModel> LatenessChart { get; set; } = new();


    private readonly LocalDBService _localDBService;

    public Appointments()
    {
        InitializeComponent();
        BindingContext = this;

        var dbPath = System.IO.Path.Combine("D:\\egyetem\\VS\\repos\\MauiApp1\\MauiApp1", "WorkersDB.db");
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
        var schedule = scheduler.AssignTasks(ScheduleMode.EDD);
        var tabuSchedule = scheduler.AssignTasks(ScheduleMode.TabuSearch);


        ScheduleResults.Clear();


        var allTasks = schedule
            .SelectMany(entry => entry.Value)
            .OrderBy(t => t.Deadline)
            .ToList();

        ScheduleResults.Add(new ScheduledTaskViewModel
        {
            AssignedTasks = allTasks.Select(t =>
                $" Készülék: {t.DeviceID} \n Feladat sorszáma: {t.TaskID} - Deadline: {t.Deadline:yyyy-MM-dd}").ToList()
        });


        TabuSearchResults.Clear();
        TabuSearchResults.Add(new ScheduledTaskViewModel
        {
            AssignedTasks = tabuSchedule
                .SelectMany(kvp => kvp.Value)
                .Select(t => $" Készülék: {t.DeviceID} \n Feladat sorszáma: {t.TaskID} - Határidõ: {t.Deadline:yyyy-MM-dd}")
                .ToList()
        });


        LatenessChart.Clear();

        var sampled = scheduler.LatenessHistory
            .Where(l => l.Iteration == 0 || l.Iteration == 10 || l.Iteration == 25 || l.Iteration == 50 || l.Iteration == 100)
            .ToList();

        foreach (var (iteration, latenessInMinutes) in sampled)
        {
            int latenessInDays = (int)Math.Round(latenessInMinutes / 1440.0);

            LatenessChart.Add(new LatenessViewModel
            {
                Iteration =iteration,
                Lateness = $"{latenessInDays} nap"
            });
        }
    }
}
