namespace ReGaSLZR
{

    using SFB;
    using TMPro;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIBankChooserRow : MonoBehaviour
    {

        [SerializeField]
        private ScriptableObject defaultSO;

        [Space]

        [SerializeField]
        private TextMeshProUGUI textInfo;

        [SerializeField]
        private Button buttonChoose;

        [SerializeField]
        private Button buttonCancel;

        [SerializeField]
        private Button buttonSetToDefault;

        [Space]

        [SerializeField]
        [Multiline]
        private string valueDefaultInfo;

        [SerializeField]
        private Color colorTextNeutral;

        [SerializeField]
        private Color colorTextPositive;

        private string pathChosenBank;

        private void Awake()
        {
            buttonCancel.onClick.AddListener(CancelChoice);
            buttonChoose.onClick.AddListener(Choose);
            buttonSetToDefault.onClick.AddListener(ConfigureDefaultSettings);
        }

        public void ConfigureDefaultSettings()
        {
            if (defaultSO == null)
            {
                CancelChoice();
            }
            else
            {
                var fullPath = string.Concat(
                    Application.dataPath.Replace(StringConstants.ASSETS_CONCATINATOR, string.Empty),
                    AssetDatabase.GetAssetPath(defaultSO));

                //Debug.Log($"{GetType().Name}.ConfigureDefaultSettings(): " +
                //    $"default SO full path is: {fullPath}", gameObject);
                ChoosePath(fullPath);
            }
        }

        public string GetPathChosenBank() => pathChosenBank;

        private void Choose()
        {
            var extensions = new[] {
            new ExtensionFilter("ScriptableObject", "asset"),
        };

            var paths = StandaloneFileBrowser.OpenFilePanel(valueDefaultInfo,
                Application.dataPath + "/" + StringConstants.RESOURCES_CONCATINATOR,
                extensions, false);

            if (paths.Length >= 1)
            {
                ChoosePath(paths[0]);
            }
            else
            {
                CancelChoice();
            }
        }

        private void ChoosePath(string path)
        {
            pathChosenBank = path;
            //Debug.Log($"{GetType().Name}.Choose(): bank chosen is: {pathChosenBank}");
            var explodedPath = pathChosenBank.Split('/');

            textInfo.text = explodedPath[explodedPath.Length - 1];
            textInfo.color = colorTextPositive;
        }

        private void CancelChoice()
        {
            textInfo.color = colorTextNeutral;
            textInfo.text = valueDefaultInfo;
            pathChosenBank = string.Empty;
        }

    }

}