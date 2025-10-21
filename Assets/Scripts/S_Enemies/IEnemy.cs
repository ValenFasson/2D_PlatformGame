public interface IEnemy
{
    // Trigger a facade action for this enemy.
    // actionName is a short identifier like "spawn", "attack", "death".
    void TriggerFacade(string actionName);
}
