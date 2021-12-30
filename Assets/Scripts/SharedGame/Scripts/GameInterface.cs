﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SharedGame {

    public class GameInterface : MonoBehaviour {
        public int maxLogLines = 20;
        public Text txtStatus;
        public Text txtChecksum;
        public Text txtGameLog;
        public Text txtPluginLog;
        public Button btnPlayer1;
        public Button btnPlayer2;
        public Button btnConnect;
        public Toggle tglPluginLog;
        public Toggle tglGameLog;
        public bool gameLog = true;
        public bool pluginLog = true;

        private GameManager gameManager => GameManager.Instance;
        private readonly List<string> gameLogs = new List<string>();
        private readonly List<string> pluginLogs = new List<string>();

        private void Awake() {
            gameManager.OnStatus += OnStatus;
            gameManager.OnChecksum += OnChecksum;
            GGPORunner.OnPluginLog += OnPluginLog;
            GGPORunner.OnGameLog += OnGameLog;
            gameManager.OnRunningChanged += OnRunningChanged;

            btnConnect.onClick.AddListener(OnConnect);
            btnPlayer1.onClick.AddListener(OnPlayer1);
            btnPlayer2.onClick.AddListener(OnPlayer2);

            tglPluginLog.isOn = false;
            tglGameLog.isOn = false;

            tglPluginLog.onValueChanged.AddListener(OnTogglePluginLog);
            tglGameLog.onValueChanged.AddListener(OnToggleGameLog);

            SetConnectText("");
        }

        private void OnDestroy() {
            gameManager.OnStatus -= OnStatus;
            gameManager.OnChecksum -= OnChecksum;
            GGPORunner.OnPluginLog -= OnPluginLog;
            GGPORunner.OnGameLog -= OnGameLog;
            gameManager.OnRunningChanged -= OnRunningChanged;

            btnConnect.onClick.RemoveListener(OnConnect);
            btnPlayer1.onClick.RemoveListener(OnPlayer1);
            btnPlayer2.onClick.RemoveListener(OnPlayer2);

            tglPluginLog.onValueChanged.RemoveListener(OnTogglePluginLog);
            tglGameLog.onValueChanged.RemoveListener(OnToggleGameLog);
        }

        private void OnRunningChanged(bool obj) {
            SetConnectText(obj ? "Shutdown" : "--");
        }

        private void OnToggleGameLog(bool value) {
            if (tglGameLog.isOn) {
                txtGameLog.text = string.Join("\n", gameLogs);
            }
            txtGameLog.gameObject.SetActive(tglGameLog.isOn);
        }

        private void OnTogglePluginLog(bool value) {
            if (tglPluginLog.isOn) {
                txtPluginLog.text = string.Join("\n", gameLogs);
            }
            txtPluginLog.gameObject.SetActive(tglPluginLog.isOn);
        }

        private void SetConnectText(string text) {
            btnConnect.GetComponentInChildren<Text>().text = text;
        }

        private void OnGameLog(string text) {
            if (gameLog) {
                Debug.Log("[GameLog] " + text);
            }
            gameLogs.Insert(0, text);
            while (gameLogs.Count > maxLogLines) {
                gameLogs.RemoveAt(gameLogs.Count - 1);
            }
            if (tglGameLog.isOn) {
                txtGameLog.text = string.Join("\n", gameLogs);
            }
        }

        private void OnPluginLog(string text) {
            if (pluginLog) {
                Debug.Log("[PluginLog] " + text);
            }
            pluginLogs.Insert(0, text);
            while (pluginLogs.Count > maxLogLines) {
                pluginLogs.RemoveAt(gameLogs.Count - 1);
            }
            if (tglPluginLog.isOn) {
                txtPluginLog.text = string.Join("\n", pluginLogs);
            }
        }

        private void OnPlayer1() {
            gameManager.DisconnectPlayer(0);
        }

        private void OnPlayer2() {
            gameManager.DisconnectPlayer(1);
        }

        private void OnConnect() {
            if (gameManager.IsRunning) {
                gameManager.Shutdown();
            }
        }

        private void OnChecksum(string text) {
            txtChecksum.text = text;
        }

        private void OnStatus(string text) {
            txtStatus.text = text;
        }
    }
}