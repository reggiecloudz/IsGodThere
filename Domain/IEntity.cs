namespace IsGodThere.Domain
{
    public interface IEntity
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
