﻿using Robust.Client.GameObjects.Components.UserInterface;
using Content.Shared.Kitchen;
using Robust.Shared.GameObjects.Components.UserInterface;

namespace Content.Client.GameObjects.Components.Kitchen
{
    public  class MicrowaveBoundUserInterface : BoundUserInterface
    {
        private MicrowaveMenu _menu;

        public MicrowaveBoundUserInterface(ClientUserInterfaceComponent owner, object uiKey) : base(owner,uiKey)
        {

        }

        protected override void Open()
        {
            base.Open();
            _menu = new MicrowaveMenu(this);
            _menu.OpenCentered();
            _menu.OnClose += Close;
        }

        protected override void UpdateState(BoundUserInterfaceState state)
        {
            base.UpdateState(state);
            if (!(state is MicrowaveUpdateUserInterfaceState cstate))
                return;
            _menu.RefreshContentsDisplay(cstate.ReagentsReagents, cstate.ContainedSolids);

        }

        public void Cook()
        {
            SendMessage(new SharedMicrowaveComponent.MicrowaveStartCookMessage());
        }

        public void Eject()
        {
            SendMessage(new SharedMicrowaveComponent.MicrowaveEjectMessage());
        }

        public void EjectSolidWithIndex(int index)
        {
            SendMessage(new SharedMicrowaveComponent.MicrowaveEjectSolidIndexedMessage(index));
        }
    }
}
