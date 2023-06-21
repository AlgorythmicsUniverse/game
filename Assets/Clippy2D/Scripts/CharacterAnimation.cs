using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Clippy2D.Scripts {
    public class CharacterAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public Sprite[] idle1Frames;// Array for idle animation 1
        public Sprite[] idle2Frames; // Array for idle animation 2
        public Sprite[] hoverInFrames; // Array for hover in animation
        public Sprite[] hoverOutFrames; // Array for hover out animation
        public float idle2Interval = 30f;
        public float frameRate = 0.1f; // Delay between each frame

        private Image _image;
        private GameObject _tips;
        private bool _allowedToClick = false;
        private int _hoverInLastFrameIndex = 0;
        private bool _isHovering = false;
        private float _idle1Timer = 0f;
        private float _idle2Timer = 0f;
        private float _hoverTimer = 0f;
        private int _idle1FrameIndex = 0;
        private int _idle2FrameIndex = 0;
        private int _hoverInFrameIndex = 0;
        private int _hoverOutFrameIndex = 0;

        private enum AnimationState {
            Idle1,
            Idle2,
            HoverIn,
            HoverOut
        }

        private AnimationState currentState = AnimationState.Idle1;

        private void Awake() {
            _image = GetComponent<Image>();
            _tips = GameObject.FindGameObjectWithTag("Tips");
            _tips.SetActive(false);
            Debug.Log(_tips.name);
        }

        private void Update() {
            switch (currentState) {
                case AnimationState.Idle1:
                    PlayIdle1Animation();
                    break;
                case AnimationState.Idle2:
                    PlayIdle2Animation();
                    break;
                case AnimationState.HoverIn:
                    PlayHoverInAnimation();
                    break;
                case AnimationState.HoverOut:
                    PlayHoverOutAnimation();
                    break;
            }
        }

        public void OnClick() {
            if (_allowedToClick) {
                // Enable the Text GameObject to show the textbox
                Debug.Log("Clicked");
                if (_tips.activeSelf) {
                    _tips.SetActive(false);
                }
                else {
                    _tips.SetActive(true);
                }
            }
        }

        private void PlayIdle1Animation() {
            _idle1Timer += Time.deltaTime;
            _idle2Timer += Time.deltaTime;

            _image.sprite = idle1Frames[_idle1FrameIndex];
            _idle1Timer += Time.deltaTime;
            _idle2Timer += Time.deltaTime;

            if (_idle1Timer >= frameRate) {
                _idle1FrameIndex = (_idle1FrameIndex + 1) % idle1Frames.Length;
                _idle1Timer = 0f;
                // Transition to Idle2 animation
                if (_idle1FrameIndex == 0 && _idle2Timer > idle2Interval) {
                    currentState = AnimationState.Idle2;
                    _idle2FrameIndex = 0;
                    _idle2Timer = 0f;
                }
            }
        }

        private void PlayIdle2Animation() {
            _image.sprite = idle2Frames[_idle2FrameIndex];
            _idle2Timer += Time.deltaTime;

            if (_idle2Timer >= frameRate) {
                _idle2FrameIndex = (_idle2FrameIndex + 1) % idle2Frames.Length;
                _idle2Timer = 0f;
                // Transition back to Idle1 animation
                if (_idle2FrameIndex == 0) {
                    currentState = AnimationState.Idle1;
                    _idle1FrameIndex = 0;
                }
            }
        }
    
    

        private void PlayHoverInAnimation() {
            _hoverTimer += Time.deltaTime;

            if (_hoverInFrameIndex < hoverInFrames.Length) {
                _image.sprite = hoverInFrames[_hoverInFrameIndex];
                _hoverTimer += Time.deltaTime;

                if (_hoverTimer >= frameRate) {
                    _hoverInFrameIndex++;
                    _hoverInLastFrameIndex = _hoverInFrameIndex;
                    _hoverTimer = 0f;
                }
            }
            else {
                // Stay on the last frame while the mouse is over the button
                if (!_isHovering) {
                    currentState = AnimationState.Idle1;
                    _idle1FrameIndex = 0;
                    _hoverInFrameIndex = 0;
                    _hoverInLastFrameIndex = hoverInFrames.Length;
                }

                if (_isHovering && _hoverInFrameIndex == hoverInFrames.Length) {
                    _allowedToClick = true;
                }
            }
        }

        private void PlayHoverOutAnimation() {
            _hoverTimer += Time.deltaTime;

            if (_hoverOutFrameIndex < hoverOutFrames.Length) {
                _image.sprite = hoverOutFrames[_hoverOutFrameIndex];
                _hoverTimer += Time.deltaTime;

                if (_hoverTimer >= frameRate) {
                    _hoverOutFrameIndex++;
                    _hoverTimer = 0f;
                }
            }
            else {
                currentState = AnimationState.Idle1;
                _idle1FrameIndex = 0;
                _hoverOutFrameIndex = 0;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (currentState != AnimationState.HoverIn) {
                currentState = AnimationState.HoverIn;
                _hoverInFrameIndex = 0;
                _hoverOutFrameIndex = 0;
                _hoverTimer = 0f;
                _isHovering = true;
                _allowedToClick = false;
            }
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (currentState != AnimationState.HoverOut) {
                currentState = AnimationState.HoverOut;
                _hoverOutFrameIndex = hoverOutFrames.Length - _hoverInLastFrameIndex - 1;
                _hoverOutFrameIndex = _hoverOutFrameIndex < 0 ? 0 : _hoverOutFrameIndex;
                _hoverInFrameIndex = 0;
                _hoverTimer = 0f;
                _isHovering = false;
                _allowedToClick = false;
            }
        }
    }
}