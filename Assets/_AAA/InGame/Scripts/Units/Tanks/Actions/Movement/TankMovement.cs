
public class TankMovement : UnitMovement
{
    protected override float Speed => 10;
    protected override float RotateSpeed => 10;

    public override void NetworkFixedUpdate()
    {
        if (!CanExecute())
            return;
        base.NetworkFixedUpdate();

        if (baseUnit is not TankUnit tankUnit)
            return;
        var moveInput = tankUnit.UserInputHandler.InputData.Movement;
        Move(moveInput);
    }
}
