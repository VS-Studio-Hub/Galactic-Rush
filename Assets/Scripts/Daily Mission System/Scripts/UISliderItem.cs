using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Venthan.DailyMission
{
    [RequireComponent(typeof(Button))]
    public class UISliderItem : MonoBehaviour
    {
        [Header(" Element ")]
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        public TextMeshProUGUI Text => text;

        private Button button;
        public Button Button => button;

        public void Configure(Sprite sprite, string label)
        {
            image.sprite = sprite;
            text.text = label;

            button = GetComponent<Button>();
        }
        //private void Update()
        //{
        //    float angle = Mathf.PingPong(Time.time * 20f, 20f) - 10f;
        //    image.transform.localRotation = Quaternion.Euler(0, 0, angle);
        //}


        //[NaughtyAttributes.Button]
        //public void Animate()
        //{
        //    LeanTween.cancel(image.gameObject);
        //    LeanTween.rotate(image.gameObject, Vector3.forward * 10, .5f).SetLoopPingPong(100);
        //}

        //[NaughtyAttributes.Button]
        //public void StopAnimation()
        //{
        //    LeanTween.cancel(image.gameObject);
        //    LeanTween.rotate(image.gameObject, Vector3.zero, .5f);
        //}
    }
}