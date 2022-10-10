namespace BetterTownOfUs.Roles
{
    public interface IVisualAlteration
    {
        bool TryGetModifiedAppearance(out VisualAppearance appearance);
    }
}