namespace Core
{
    public abstract class GameState
    {
        protected GameStateController _controller;

        protected GameState(GameStateController controller)
        {
            _controller = controller;
        }

        public abstract void Enter();
        public abstract void Update();
        
        /// <summary>
        /// This state should dispose and shutdown all resoruces
        /// </summary>
        protected abstract void Exit();

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}