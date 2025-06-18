using UnityEngine;
using DG.Tweening;
using UnityEngine.U2D;
using DefaultNamespace.Events;
using Unity.Mathematics;

public class Card : MonoBehaviour
{
    public int cardRank;
    public string cardSuit;
    public bool isAvailable;
    public bool isSelected = false;
    public bool isInteractible = false;
    public bool isInHand = false;
    private PlayerHand _playerHand;

    [Header("Rendering")]
    [SerializeField] private SpriteAtlas _atlas;
    private MeshRenderer _meshRenderer;
    private Material _material;
    private Sprite _defaultTexture;
    private Sprite _hoverTexture;

    [Header("Animation")]
    [SerializeField] private float _hoverDistance;
    [SerializeField] private float _hoverTime;
    [SerializeField] private Vector3 _playerCardPlacementPoint;
    [SerializeField] private float _playerCardPlacementTime;
    [SerializeField] private float _onArrangeHandTime;

    private void Awake()
    {
        // declare the hand
        _playerHand = FindObjectOfType<PlayerHand>();

        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    // hovering over the card when it's in hand.
    private void OnMouseEnter()
    {
        if (isInHand && isInteractible)
        {
            _material.mainTexture = _hoverTexture.texture;
            AnimHoverUp();
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && isInteractible)
        {
            if(isSelected == true)
            {
                _playerHand.PlaySelectedCard(this);
            }
            _material.mainTexture = _hoverTexture.texture;

            _playerHand.CheckSelectedCard();
            isSelected = !isSelected;
        }
    }

    private void OnMouseExit()
    {
        if (isInteractible)
        {
            if (isInHand && isSelected == false)
            {
                _material.mainTexture = _defaultTexture.texture;
                AnimHoverDown();
            }
        }

    }

    /// <summary>
    /// Rendering the face up side of the cards
    /// </summary>
    public void RenderCard()
    {
        Sprite[] sprites = new Sprite[_atlas.spriteCount];
        _atlas.GetSprites(sprites);

        // choose the corresponding card sprites
        foreach (Sprite check in sprites)
        {
            string checkName = check.name.Replace("(Clone)", "");
            string hover = $"{cardRank}_{cardSuit}_1";
            string def = $"{cardRank}_{cardSuit}_2";

            if (checkName == hover)
            {
                _hoverTexture = check;
            }

            if (checkName == def)
            {
                _defaultTexture = check;
                _material.mainTexture = check.texture;
                _material.mainTextureOffset = UVToOffset(check.uv);
                _material.mainTextureScale = UVToScale(check.uv);
            }
        }
    }

    private Vector2 UVToOffset(Vector2[] uv)
    {
        Vector2 offset = uv[2];
        return offset;
    }

    private Vector2 UVToScale(Vector2[] uv)
    {
        Vector2 scale = uv[1] - uv[2];
        return scale;
    }

    /// <summary>
    /// Card animations
    /// </summary>
    public void AnimHoverUp()
    {
        transform.DOLocalMoveY(_hoverDistance, _hoverTime);
    }
    
    public void AnimHoverDown()
    {
        transform.DOLocalMoveY(0, _hoverTime);
    }
    
    public void AnimPlayToTable()
    {
        transform.DOMove(_playerCardPlacementPoint, _playerCardPlacementTime);
    }

    public void AnimOnMoveAndRotate(Vector3 newPosition,Quaternion newRotation, float delay)
    {
        transform.DOLocalMove(newPosition, _onArrangeHandTime).SetDelay(delay).SetEase(Ease.InOutCubic);
        transform.DOLocalRotateQuaternion(newRotation, _onArrangeHandTime).SetDelay(delay).SetEase(Ease.InOutCubic);
    }
    
    public void AnimOnRotate(Quaternion newRotation, float delay)
    {
        transform.DOLocalRotateQuaternion(newRotation, _onArrangeHandTime).SetDelay(delay).SetEase(Ease.InOutCubic);
    }
    
    public void AnimOnMove(Vector3 newPosition, float delay)
    {
        transform.DOLocalMove(newPosition, _onArrangeHandTime).SetDelay(delay).SetEase(Ease.InOutCubic);
    }

}
