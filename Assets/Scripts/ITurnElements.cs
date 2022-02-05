using System;

namespace DefaultNamespace
{
    public interface ITurnElements
    {
        void AddTurnOnAction(Action turnOnAction);

        void AddTurnOffAction(Action turnOffAction);

        bool isTurnedOn();
    }
}