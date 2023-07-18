using SingleSaver;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Text intText;
        [SerializeField] private Text floatText;
        [SerializeField] private Text stringText;
        [SerializeField] private Text vector2Text;
        [SerializeField] private Text vector3Text;
        [SerializeField] private Text stringListText;

        [SerializeField] private SingleSaverKeysManager singleSaver;

        private int intData;
        private float floatData;
        private string stringData;
        private Vector2 vector2Data;
        private Vector3 vector3Data;
        private List<string> listStringData;

        private void Start()
        {
            singleSaver = SingleSaverKeysManager.Instance;
            singleSaver.DeleteAllData();

            intData = 10;
            floatData = 10.0f;
            stringData = "ten";
            vector2Data = new Vector2(10, 10);
            vector3Data = new Vector3(10, 10, 10);
            listStringData = new List<string>
            {
                "item0",
                "item1"
            };

            singleSaver.SetData("intData", intData);
            singleSaver.SetData("floatData", floatData);
            singleSaver.SetData("stringData", stringData);
            singleSaver.SetData("vector2Data", vector2Data);
            singleSaver.SetData("vector3Data", vector3Data);
            singleSaver.SetData("listStringData", listStringData);

            intData = singleSaver.GetIntData("intData");
            floatData = singleSaver.GetFloatData("floatData");
            stringData = singleSaver.GetStringData("stringData");
            vector2Data = singleSaver.GetVector2Data("vector2Data");
            vector3Data = singleSaver.GetVector3Data("vector3Data");
            listStringData = singleSaver.GetStringListData("listStringData");

            FillText();
        }

        private void FillText()
        {
            intText.text = $"intData: {intData}";
            floatText.text = $"floatData: {floatData}";
            stringText.text = $"stringData: {stringData}";
            vector2Text.text = $"vector2Data: Vector2({vector2Data.x};{vector2Data.y})";
            vector3Text.text = $"vector3Data: Vector3({vector3Data.x};{vector3Data.y};{vector3Data.z})";
            stringListText.text = $"stringListData: {listStringData[0]}, {listStringData[1]}";
        }
    }
}