---
tags:
  - ToDo
---

## Production
### Day 1:
- [>] Audio asset list
- [x] Art asset list
- [x] Animation asset list 
	- [[ArtAssetList#Animation List|Animations]]
- [x] VFX asset list
- [ ] Asset credits document
### Day 2:
- [ ] Spreadsheet to visualize upgrades and values
- [ ] Slides if we're doing that
### Day 3:
1. [ ] Trailer if we're doing that
2. [ ] Build if we're doing that
3. [ ] Itch.io page if we're doing that
## Design
### Day 1:
1. [x] Decide visual theme
2. [x] Track layout
### Day 2:
1. [ ] Create rider path using plugin
### Day 3:
1. [ ] Decorate environment
## Programming
### Day 1:
1. [x] Install packages and test VR works
2. [x] Get basic non-networked avatar set up
3. [x] Network avatar components
4. [x] Connect two clients on one machine
5. [x] Connect two different machines
6. [x] Implement haptics system
### Day 2:
1. [!] Networked gun mechanic
	1. [ ] Fix Burst
	2. [ ] Tracers
	3. [ ] Reload
	4. [x] Raycast uses hit target script
2. [ ] Networked targets
	1. [x] Confirm despawn
	2. [!] Confirm spawns from scene for both clients
	3. [ ] Confirm interaction with gun's raycast
3. [ ] Make score system non-networked
	1. [x] Confirm score adding works
	2. [ ] Make each client's score reference available to the game manager
	3. [>] Make each client's score reference available to the shop system?
4. [ ] Introduce game manager and core game loop
	1. [ ] Hand + trigger based ready up system (copy code from reload system)
	2. [ ] Sync'd ride start for both players
	3. [ ] Shop with score system
	4. [ ] Continue next ride segment after shop phase (shop phases timer based, 10s to start)
5. [ ] Basic menu navigation
	1. [ ] Stationary rig with raycast to world space menus
		1. [ ] Lobby code join/create system
		2. [ ] Credits
		3. [ ] Quit
	2. [ ] Quick options menu on forearm (wristwatch style)
		1. [ ] Retire (quit, only if in a match)
		2. [ ] Volume sliders (music, sfx)
		3. [ ] ==Red Light== Change handedness (swap options menu and gun)
6. [ ] Upgrading grub model implementation
	1. [ ] Gun mod anchor transforms
		1. [ ] Center of head (eyes, horns, ears, hats)
		2. [ ] Base of head (root for body attachment point)
		3. [ ] Front, back, top, and under side of body (additional body segments, pouch, head/tail, spine spikes)
		4. [ ] Base of tail (root for body attachment point)
		5. [ ] Between grub arms (carrying things)
	3. [ ] Gun mod manager script
		1. [ ] Uses scriptable objects for mods/upgrades
			1. [ ] Enum for anchor location (CenterHead, BaseHead, FrontBody, BackBody, TopBody, UnderBody, BaseTail, CenterHands)
			2. [ ] Bool for replace (false means additive)
			3. [ ] Prefab (if applicable) 
				- tail upgrades for clip size will use sequential prefabs with each having one additional light than the last
				- tail light system will need to be standalone and reference a gun script to determine how much ammo it has and modify its lights accordingly
### Day 3:
1. [ ] Networked audio implementation
3. [ ] Networked animations (if we end up with animations)
4. [ ] Networked VFX implementation
## Art
### Day 1:
- [ ] Acquire any assets we can
### Day 2: 
- [x] Talk to Rachel about grub gun
### Day 3:
- [ ] Post processing
## Audio
### Day 1:
1. [ ] Create audio asset list
### Day 2:
2. [ ] Borrow assets
### Day 3:
3. [ ] Implement borrowed assets