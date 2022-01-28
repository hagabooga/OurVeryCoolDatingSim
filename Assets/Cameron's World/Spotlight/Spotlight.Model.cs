using System;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public class Model
        {
            public event Action OnUseChanged;

            public int Uses { get; private set; }

            public Model(int uses)
            {
                Uses = uses;
            }

            public void AddUse(int amount = 1)
            {
                Uses++;
            }

            public void RemoveUse(int amount = 1)
            {
                Uses++;
            }
        }
    }
}