﻿using System.Collections.Generic;
using TP0.Entity;
using Newtonsoft.Json;
using System;
using System.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace TP0.Loader
{
    class GladiatorConverter : CustomCreationConverter<Gladiator>
    {
        public override Gladiator Create(Type objectType)
        {
            return new Gladiator();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jsonObject = JObject.Load (reader);

			return this.loadGladiator (jsonObject, new Gladiator ());
		}

       	private Gladiator loadGladiator(JObject jsonObject, Gladiator gladiator)
        {
            gladiator.name = jsonObject.Property("name").ToString();
            gladiator.nbMatchPlayed = uint.Parse(jsonObject.Property("nb_match_played").Value.ToString());
            gladiator.nbMatchWon = uint.Parse(jsonObject.Property("nb_match_won").Value.ToString());
			List<int> weapons = jsonObject.Property("weapons").ToArray()[0].ToObject<List<int>>();
			List<int> armors = jsonObject.Property("armors").ToArray()[0].ToObject<List<int>>();

			LoadWeapon loaderWeapon = new LoadWeapon();
			loaderWeapon.loadAll(gladiator, weapons);

			LoadArmor loaderArmor = new LoadArmor();
			loaderArmor.loadAll(gladiator, armors);

			// TODO Check if gladiator is valid

            return gladiator;
        }
    }

    class LoadGladiator
    {
        public LoadGladiator() {}

        public Gladiator loadOne(string name)
        {
            // TODO check if file exists
            string stringJson = File.ReadAllText("Assets/Gladiator/" + name + ".json");

            return JsonConvert.DeserializeObject<Gladiator>(stringJson, new GladiatorConverter());
        }
    }
}
