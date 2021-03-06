﻿using TP0.Utility;
using TP0.Stuff;
using TP0.Debuff;
using System;
using System.Collections.Generic;

namespace TP0.Entity
{
	public class Gladiator
	{
		private string _name;
		private Stuff _stuff;
		private IDebuffAttack _debuff;
		private uint _nbMatchPlayed;
		private uint _nbMatchWon;

		public Stuff stuff {
			get {
				return this._stuff;
			}
		}
		public IDebuffAttack debuff 
		{
			get {
				return this._debuff;
			}
			set {
				this._debuff = value;
			}
		}
        public string name
        {
            get {
                return this._name;
            }
            set {
                this._name = value;
            }
        }
        public uint nbMatchWon
        {
            get {
                return this._nbMatchWon;
            }
            set {
                this._nbMatchWon = value;
            }
        }
        public uint nbMatchPlayed
        {
            get {
                return this._nbMatchPlayed;
            }
            set {
                this._nbMatchPlayed = value;
            }
        }

        public Gladiator ()
		{
			this._stuff = new Stuff();
			this._debuff = new NoDebuff();
		}

		public bool addItem (Item item, SlotStuff slotStuff)
		{			
			CheckIfGladiatorCanEquip check = new CheckIfGladiatorCanEquip();

			if (check.canEquip(this, item, slotStuff)) {
				this._stuff.addItem(slotStuff, item);
				return true;
			}

			return false;
		}

        public bool isFilet(SlotStuff slotStuff)
        {
            return (this._stuff.isFilet(slotStuff));
        }

		public uint chanceHit (SlotStuff slotStuff)
		{
            uint chance = this._stuff.getTotalValueOfStatsForOnePiece(TypeStats.CHANCE_HIT, slotStuff);

            if (this.isFilet(slotStuff)) {
                this._stuff.deleteItem(slotStuff);
            }

            return this._debuff.decreaseAttack(chance);
		}

		public uint chanceParry (SlotStuff slotStuff)
		{
			return this._stuff.getTotalValueOfStatsForOnePiece(TypeStats.CHANCE_PARRY, slotStuff);
		}

		public float pourcentageVictory ()
		{
            if (0 == this._nbMatchPlayed) {
                return 0f;
            } else {
                return (this._nbMatchWon / this._nbMatchPlayed) * 100;
            }
		}

        public bool isAlive()
        {
            return !(this._debuff is Dead);
        }

        public List<SlotStuff> getSlotByStats(TypeStats stats)
        {
            List<SlotStuff> hitItems = new List<SlotStuff>();
            var slots = Enum.GetValues(typeof(SlotStuff));

            foreach(SlotStuff slot in slots) {
                if (0 != this._stuff.getTotalValueOfStatsForOnePiece(stats, slot)) {
                    hitItems.Add(slot);
                }
            }

            return hitItems;
        }

		public void addMatch ()
		{
			this._nbMatchPlayed++;
		}

        public void addVictory ()
        {
            this._nbMatchWon++;
        }

		public bool isValid ()
		{
			return this._stuff.hasWeapon();
		}

        public override string ToString()
        {
			return "[Gladiator] : " + this._name + " stuff : " + this._stuff;
        }
    }
}

