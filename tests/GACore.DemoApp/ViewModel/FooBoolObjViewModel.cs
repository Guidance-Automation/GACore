using GACore.Command;
using GACore.DemoApp.Model;
using System.Runtime.Versioning;
using System.Windows.Input;

namespace GACore.DemoApp.ViewModel;

[SupportedOSPlatform("windows")]
public class FooBoolObjViewModel : AbstractViewModel<FooBoolObj>
{
	public bool IsSet
	{
		get { return Model != null ? Model.IsSet : false; }
		private set
		{
			if (Model != null) Model.ToggleIsSet();
			OnNotifyPropertyChanged();
		}
	}

	protected override void HandleModelUpdate(FooBoolObj oldValue, FooBoolObj newValue)
	{
		OnNotifyPropertyChanged("IsSet");
		base.HandleModelUpdate(oldValue, newValue);
	}

	public ICommand ClickCommand { get; set; }

	private void HandleCommands()
	{
		ClickCommand = new CustomCommand(ClickCommandClick, CanClickCommandClick);
	}

    private void ClickCommandClick(object obj)
    {
        IsSet = false; // Forces recalc
    }

    private bool CanClickCommandClick(object obj)
    {
        return true;
    }

    public FooBoolObjViewModel()
	{
		HandleCommands();
	}
}
