namespace GACore.Test;

public class FooViewModel : AbstractViewModel<FooModel>
{
	public FooViewModel()
	{
	}

	protected override void HandleModelUpdate(FooModel? oldValue, FooModel? newValue)
	{
		base.HandleModelUpdate(oldValue, newValue);
	}
}