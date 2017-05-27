﻿using System;
using System.Collections.Generic;
using AbstractLibrary.Pattern;
using ElectionLibrary.Character;

namespace ElectionLibrary.Environment
{
    public class PublicPlace : AbstractElectionArea, IObservable
    {
        public readonly List<IObserver> Buildings;

        private PoliticalCharacter LastPolitician { get; set; }

        public PublicPlace(Opinion opinion, string name, Position position) : base(opinion, name, position)
        {
            Buildings = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            Buildings.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Buildings.Remove(observer);
        }

        public void Notify()
        {
            foreach(Building building in Buildings)
            {
                building.Update(LastPolitician);
            }
        }

        public override void ChangeOpinion(PoliticalCharacter politician)
        {
            opinion.InfluenceOpinion(politician.PoliticalParty, politician.Aura, politician.Moral, 1);
            LastPolitician = politician;
            Notify();
        }
    }
}
