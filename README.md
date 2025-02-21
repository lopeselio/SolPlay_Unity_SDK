# SolPlay - Unity - SDK
An example project to show how to use Phantom deeplinks to login and use Solana NFTs in Unity games and apps.

Follow me on Twitter for more frequent updates: @SolPlay_jonas

1) To use it in your game just include in the Unity Package manager: 
https://github.com/Woody4618/Solana.Unity-SDK.git (maybe a few commits ahead or behind the garbles SDK) 
or 
https://github.com/garbles-labs/Solana.Unity-SDK.git
in your unity package manager.

2) Import this unity package (Version 0.0.9 alpha)
[SolPlay_0_0_9.unitypackage.zip](https://github.com/Woody4618/SolPlay_Unity_SDK/files/10287656/SolPlay_0_0_9.unitypackage.zip)
and then import it in Unity Assets->importPackage.

Some function may not work with the standart MainNet RPC. 
You may want to get a free RPC from quicknode. 
Also currently its hard to get DevNet sol, so if the automatic airdrops fail you may need to try a few times, try different RPC in the WalletHolderService or transfer dev net sol to your Wallet. (The publickey will be logged in the console on login) 


3) Optional: Import Glftfast for 3D NFT support
[GlFAst Installer](https://package-installer.glitch.me/v1/installer/OpenUPM/com.atteneder.gltfast?registry=https%3A%2F%2Fpackage.openupm.com&scope=com.atteneder)
Also add the precompiler flag #GLTFAST


Here is a Video which explains the process step by step: (A bit out dated, you can now skip the step 3) 
[https://www.youtube.com/channel/UC517QSv61gMaABWIJ412_Lw/videos](https://youtu.be/mS5Fx_yzcHw)

Release notes:
0.0.9 Alpha
- Faster NFt loading by seperating Json from the Image loading and using UniTasks to yield tasks in WebGL
- Socket connection now works with all RPC providers I know. Check out the SolPlaySocketService
- All folders are not within the SolPlay folder to have less clutter in the root folder 
- Tiny AdvenureTutorial Anchor example client (https://beta.solpg.io/tutorials/tiny-adventure)
- Source code of SolHunter realtime multiplayer game
- Made GLTFast dependency optional: Add GLTFAST compiler flag and install the package to use it 

If you want to participate, it's very welcome.


Packages used: 

Native WebSockets by Endel:
https://github.com/endel/NativeWebSocket

WebSocketSharp: 
https://github.com/sta/websocket-sharp

Epic Toon FX:
https://assetstore.unity.com/packages/vfx/particles/epic-toon-fx-57772

glTFast for runtime loading of 3D NFTs:
https://github.com/atteneder/glTFast

Lunar console (Get the pro version here: 
https://github.com/SpaceMadness/lunar-unity-console
Pro Version: https://assetstore.unity.com/packages/tools/gui/lunar-mobile-console-pro-43800)

Garbels unity solana sdk. Check out their awesome game as well! Vr Pokemon! 
https://github.com/garbles-dev/Solana.Unity/tree/master/src

Solanart:
https://github.com/allartprotocol/unity-solana-wallet

Tweetnacl (removed):
https://github.com/dchest/tweetnacl-js/blob/master/README.md#random-bytes-generation

Gif loading:
https://github.com/3DI70R/Unity-GifDecoder

Flappy Bird Game: 
https://github.com/diegolrs/Flappy-Bird

Unity Ui Extensions:
https://github.com/JohannesDeml/unity-ui-extensions.git

UniTask to be able to have delays in WebGL: 
https://github.com/Cysharp/UniTask/releases

Unity mainThread dispatcher to be able to call unity function from socket messages:
https://github.com/PimDeWitte/UnityMainThreadDispatcher

Anchor to C# code generation
https://github.com/garbles-labs/Solana.Unity.Anchor
https://github.com/bmresearch/Solnet.Anchor/

So far the repository is only tested in IOS mobile, Android and WebGL.

Done:
- Login and getting Public key from phantom
- Loading and caching NFTs
- Nft meta data parsing + power level calculation
- Deeplink to minting page
- Deeplink to raydium token swap
- Transactions
- In game token and sol amount loading widget
- WebGL support 
- IOS Support 
- Android Support
- Smart contract interaction
- Token swap using Orca WhirlPools
- Minting NFTs using metaplex (Without candy machine)
- Minting NFTs (from candy machine V2)
- Socket connection so listen to account updates
- Faster NFT loading by seperating the json loading from loading the images and starting multipl at the same time with a smaller rate limit
- Two anchor example games with source code here: https://beta.solpg.io/tutorials/
- 3D NFts using GLTF fast. Needs to have a glb file in the animation url of the NFT
- InGame auto approve wallet. Check our the SolHunter example for how it works. 


Next up Todo: 

- Animated Gifs
- Minting NFTs (from candy machine V2 with white list tokens)
- Maybe Staking? 
- What else would you like?  
- Try back porting the socket solution to the Garbles SDK to be able to use StreamingRPCs
- Tiny Adventure Two example 



