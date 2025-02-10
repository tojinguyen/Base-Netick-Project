using Netick;

public class SkillWithStateInfo : SkillBase
{
    [Networked] protected SkillStateInfo skillStateInfo { get; set; }

    public override void NetworkFixedUpdate()
    {
        base.NetworkFixedUpdate();
        var stateInfo = skillStateInfo;
        if (stateInfo.State == SkillState.None)
            return;
        
        stateInfo.Duration -= Sandbox.FixedDeltaTime;
        skillStateInfo = stateInfo;
        
        SkillUpdate(stateInfo);
        
        if (stateInfo.Duration <= 0)
        {
            OnEndState(skillStateInfo);
        }
    }
    
    protected virtual void SkillUpdate(SkillStateInfo curStateInfo)
    {
    }
    
    /// <summary>
    /// Override this method to control flow of the skill
    /// </summary>
    /// <param name="oldStateInfo"></param>
    protected virtual void OnEndState(SkillStateInfo oldStateInfo)
    {
    }

    protected void ChangeState(SkillState state, float duration)
    {
        skillStateInfo = new SkillStateInfo
        {
            State = state,
            Duration = duration
        };
    }
}

public struct SkillStateInfo 
{
    public SkillState State;
    public float Duration;
}
