using Roguelike.Engine;
using Roguelike.Engine.ObjectsOnMap;
using Roguelike.GameConfig.GUIElements;
using System;
using System.Collections.Generic;

namespace Roguelike.Client
{
    public class ButtonManager
    {
        private readonly List<Button> Allbuttons = new();
        private readonly List<Button> inventoryItemButtons = new();
        private readonly List<Button> popupMenuButtons = new();
        //private readonly List<Button> pocketPopupMenuButtons = new();
        public ButtonManager()
        {

        }

        public void Add(Button button)
        {
            Allbuttons.Add(button);
        }
        public void RemoveInventoryButtons()
        {
            foreach (Button button in inventoryItemButtons)
            {
                Allbuttons.Remove(button);
            }
            inventoryItemButtons.Clear();
        }
        public void AddHandInventoryButton(int handIndex,Action rightClickAction, Action leftClickAction)
        {
            HandInventoryGUI handInventoryGUI = HandInventoryGUI.GetHand(handIndex);
            int X = handInventoryGUI.X;
            int Y = handInventoryGUI.Y;
            int width = handInventoryGUI.Width;
            int height = handInventoryGUI.Height;
            
            Button handButton = new(X, Y, width, height,
                                           leftClickAction, rightClickAction);
            inventoryItemButtons.Add(handButton);
            Allbuttons.Add(handButton);
        }
        public void AddPocketInventoryButton(int index, Action rightClickAction, Action leftClickAction)
        {
            int width = PocketsInventoryBox.EntryWidth;
            int height = PocketsInventoryBox.EntryHeight;
            int X = PocketsInventoryBox.X;
            int Y = PocketsInventoryBox.Y + 1 + index;

            Button handButton = new(X, Y, width, height,
                                       leftClickAction, rightClickAction);
            inventoryItemButtons.Add(handButton);
            Allbuttons.Add(handButton);
            
        }
        public void AddHandPopupMenu(int handIndex, Action[] actions)
        {
            if(actions.Length != HandPopupMenu.numberOfOptions)
            {
                throw new Exception("wrong number of actions given");
            }

            HandInventoryGUI[] handGUI = {new RightHandInventoryGUI(), new LeftHandInventoryGUI() };
            for (int i = 0; i < actions.Length; i++)
            {
                int X = handGUI[handIndex].X + HandPopupMenu.arrowOffestX + HandPopupMenu.optionStartOffsetX;
                int Y = handGUI[handIndex].Y + HandPopupMenu.arrowOffestY + HandPopupMenu.optionStartOffsetY + i; ;
                int width = HandPopupMenu.boxWidth - 2;
                int height = 1;
                Action emptyAction = () => { };
                Button button = new Button(X, Y, width, height, actions[i], emptyAction);
                popupMenuButtons.Add(button);
                Allbuttons.Add(button);
            }
        }
        public void AddPocketPopupMenu(int inventoryItemIndex, Action[] actions)
        {
            if (actions.Length != PocketPopupMenu.numberOfOptions)
            {
                throw new Exception("wrong number of actions given");
            }

            for (int actionIndex = 0; actionIndex < actions.Length; actionIndex++)
            {
                int X = 
                      PocketsInventoryBox.X 
                    + PocketPopupMenu.optionStartOffsetY 
                    + PocketPopupMenu.arrowOffestX;
                int Y = 
                      PocketsInventoryBox.Y + 1
                    + PocketPopupMenu.optionStartOffsetY 
                    + PocketPopupMenu.arrowOffestY
                    + inventoryItemIndex 
                    + actionIndex;
                int width = PocketPopupMenu.boxWidth - 2;
                int height = 1;
                Action emptyAction = () => { };
                Button button = new Button(X, Y, width, height, actions[actionIndex], emptyAction);
                popupMenuButtons.Add(button);
                Allbuttons.Add(button);
            }
        }
        public void RemovePopupButtons()
        {
            foreach (Button button in popupMenuButtons)
            {
                Allbuttons.Remove(button);
            }

            popupMenuButtons.Clear();
        }
        public bool TryClick(int X, int Y, bool isLeftClick)
        {
            foreach (Button button in Allbuttons)
            {
                if (button.TryPress(X, Y, isLeftClick))
                {
                    return true;
                }
            }
            return false;
        }
        public bool TryClickPopups(int X, int Y, bool isLeftClick)
        {
            foreach (Button button in popupMenuButtons)
            {
                if (button.TryPress(X, Y, isLeftClick))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
