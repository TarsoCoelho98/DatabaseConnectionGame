using Assets.Scripts.Persistence.DAL.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Persistence
{
    public class GameCodeDataSource : SqlDataSource
    {
        public WeaponDAO WeaponDAO { get; protected set; }
        public CharacterDAO CharacterDAO { get; protected set; }

        private static GameCodeDataSource instance { get; set; }
        public static GameCodeDataSource Instance
        { 
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameCodeDataSource>();

                    if (instance == null)
                    {
                        var gameObject = new GameObject("GameCodeDataSource");
                        instance = gameObject.AddComponent<GameCodeDataSource>();

                        DontDestroyOnLoad(gameObject);
                    }
                }

                return instance;
            }
        }

        private void Awake()
        {
            this.DatabaseName = "GameCode.db";
            this.isCopyDataBaseMode = false;

            WeaponDAO = new WeaponDAO(this);
            CharacterDAO = new CharacterDAO(this);

            try
            {
                base.Awake();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
