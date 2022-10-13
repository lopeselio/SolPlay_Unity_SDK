using System;
using System.Numerics;
using Frictionless;
using SolPlay.Deeplinks;
using SolPlay.DeeplinksNftExample.Scripts;
using SolPlay.Engine;
using SolPlay.Orca.OrcaWhirlPool;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrcaSwapPopup : BasePopup
{
    public TMP_InputField TokenInputA;
    public TMP_InputField TokenInputB;
    public TMP_InputField AmountInput;
    public TextMeshProUGUI TokenSymbolA;
    public TextMeshProUGUI TokenSymbolB;
    public Image IconA;
    public Image IconB;
    public Button SwapButton;
    public Button SwapAAndBButton;
    public float DefaultSwapValue = 0.1f;

    private bool AToB = true;
    private PoolData currentPoolData;

    private void Awake()
    {
        ServiceFactory.Instance.RegisterSingleton(this);
        SwapButton.onClick.AddListener(OnSwapButtonClicked);
        SwapAAndBButton.onClick.AddListener(OnSwapAAndBButtonClicked);
        base.Awake();
        TokenInputA.onValueChanged.AddListener(OnInputValueChanged);
        TokenInputA.text = TokenInputB.text = DefaultSwapValue.ToString();
    }

    private void OnInputValueChanged(string newValue)
    {
        if (!float.TryParse(AmountInput.text, out float value))
        {
            ServiceFactory.Instance.Resolve<LoggingService>()
                .LogWarning($"Wrong input value {value} {currentPoolData.SymbolA} to {currentPoolData.SymbolB}", true);
            return;
        }

        UpdateContent();
    }

    public void Open(PoolData poolData)
    {
        Open();
        currentPoolData = poolData;
        AToB = true;
        TokenInputA.text = DefaultSwapValue.ToString();
        ;
        UpdateContent();
    }

    private void UpdateContent()
    {
        if (currentPoolData == null)
        {
            return;
        }

        if (!float.TryParse(AmountInput.text, out float value))
        {
            ServiceFactory.Instance.Resolve<LoggingService>()
                .LogWarning($"Wrong input value {value} {currentPoolData.SymbolA} to {currentPoolData.SymbolB}", true);
            return;
        }

        BigInteger bigInt = currentPoolData.Pool.SqrtPrice;
        var d = Double.Parse(bigInt.ToString());
        var fromX64 = Math.Pow(d * Math.Pow(2, -64), 2);
        var aRatioA = Math.Pow(10,
            currentPoolData.TokenMintInfoA.Data.Parsed.Info.Decimals -
            currentPoolData.TokenMintInfoB.Data.Parsed.Info.Decimals);
        var priceBtoA = fromX64 * aRatioA;

        TokenSymbolA.text = AToB ? currentPoolData.SymbolA : currentPoolData.SymbolB;
        TokenSymbolB.text = AToB ? currentPoolData.SymbolB : currentPoolData.SymbolA;
        IconA.sprite = AToB ? currentPoolData.SpriteA : currentPoolData.SpriteB;
        IconB.sprite = AToB ? currentPoolData.SpriteB : currentPoolData.SpriteA;

        var aToB = (value * priceBtoA).ToString("F6");
        var bToA = (value / priceBtoA).ToString("F6");
        Debug.Log($"Price a to b {aToB} b to a: {bToA}");
        TokenInputB.text = AToB ? aToB : bToA;
    }

    private async void OnSwapButtonClicked()
    {
        if (!float.TryParse(AmountInput.text, out float value))
        {
            ServiceFactory.Instance.Resolve<LoggingService>()
                .LogWarning($"Wrong input value {value} {currentPoolData.SymbolA} to {currentPoolData.SymbolB}", true);
            return;
        }

        var pow = (ulong) Math.Pow(10,
            AToB
                ? currentPoolData.TokenMintInfoA.Data.Parsed.Info.Decimals
                : currentPoolData.TokenMintInfoB.Data.Parsed.Info.Decimals);
        var floorToInt = value * pow;
        ulong valueLong = (ulong) floorToInt;

        string fromToMessage =
            AToB
                ? $"Swapping {value} {currentPoolData.SymbolA} to {currentPoolData.SymbolB}"
                : $"Swapping {value} {currentPoolData.SymbolB} to {currentPoolData.SymbolA}";

        ServiceFactory.Instance.Resolve<LoggingService>().Log(fromToMessage, true);
        var wallet = ServiceFactory.Instance.Resolve<WalletHolderService>().BaseWallet;
        var signature = await ServiceFactory.Instance.Resolve<OrcaWhirlpoolService>()
            .Swap(wallet, currentPoolData.Pool, valueLong, AToB);
        ServiceFactory.Instance.Resolve<TransactionService>().CheckSignatureStatus(signature,
            () => { ServiceFactory.Instance.Resolve<MessageRouter>().RaiseMessage(new TokenValueChangedMessage()); });
    }

    private void OnSwapAAndBButtonClicked()
    {
        AToB = !AToB;
        TokenInputA.text = TokenInputB.text;
        UpdateContent();
    }
}