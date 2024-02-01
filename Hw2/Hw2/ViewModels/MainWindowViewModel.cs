namespace Hw2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Greeting = RunDistinctIntegers();
    }
    private string RunDistinctIntegers() // this is your method
    {
        //TODO: finish the method
        return MyApplication.Run();
    }
    public string Greeting { get; set;}
}
