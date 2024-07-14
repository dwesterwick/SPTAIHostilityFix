# Mod Overview
Fixes the EFT bug introduced in SPT 3.9.0 that causes:
* PMC's and Scavs to sometimes not fight each other
* PMC's to sometimes not fight you as a player Scav if your Fence rep is 6+

# Mod Usage
This is ONLY required in any of the following cases:
* You're using SPT 3.9.0
* You're using SPT 3.9.1 before the hotfix applied on 7/14/24
* You're using SPT 3.9.x and [SAIN](https://hub.sp-tarkov.com/files/file/1062-sain-solarint-s-ai-modifications-full-ai-combat-system-replacement/) <3.0.4

This is not needed and will not work on SPT versions outside of 3.9.x.

# Mod Description
This mod patches the method that runs as bots become activated, and it checks what alive and activated players (both human and AI) in the raid should be enemies with them. If it should be enemies with the player, it explicitly adds them to its enemies list in its "memory". The patch does not explicitly force bots to be enemies for the following cases:
* One of the bots is Zryachiy or his followers
* One of the bots is the BTR
* One of the bots is Santa
* One of the bots is a USEC PMC and the other is a Rogue
