using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Player;
using MapMagic.Core;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc {
    /**
     * Simple helper class used to get a test environment with a playable ship and working network
     * without having to go through the menus etc.
     */
    public class TestScene : MonoBehaviour {

        public Transform spawnLocation;
        public ShipPlayer shipPlayerPrefab;
        
        private void Awake() {
            SceneManager.LoadScene("Engine", LoadSceneMode.Additive);
        }

        private void Start() {
            IEnumerator StartGame() {
                // allow game state to initialise
                yield return new WaitForEndOfFrame();
                
                // instruct the server to create a ship player immediately on start
                Game.Instance.SessionStatus = SessionStatus.Development;
                
                // start server and connect to it
                NetworkServer.dontListen = true;
                FdNetworkManager.Instance.StartHost();

                yield return new WaitForEndOfFrame();
                
                // enable input and position it where this entity is
                var player = ShipPlayer.FindLocal;
                if (player) {
                    player.User.EnableGameInput();
                    var pos = spawnLocation.position;
                    player.AbsoluteWorldPosition = pos;
                    Game.Instance.LoadedLevelData.startPosition = new LevelDataVector3<float>(pos.x, pos.y, pos.z);
                }
                
                // if there's a map magic object going on here, enable it
                var mapMagic = FindObjectOfType<MapMagicObject>();
                if (mapMagic) {
                    mapMagic.enabled = true;
                }
                
                // apply graphics options
                Game.Instance.ApplyGraphicsOptions();
                
                // create a test other player
                CreateTestSecondShip();
                
                // My work here is done
                gameObject.SetActive(false);
            }

            StartCoroutine(StartGame());
        }
        
        private void CreateTestSecondShip() {
            var player = ShipPlayer.FindLocal;
            if (player) {
                Instantiate(shipPlayerPrefab, player.transform.position + new Vector3(0, 0, 10), Quaternion.identity);
            }
        }
    }
}