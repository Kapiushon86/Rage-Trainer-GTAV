using GTA;
using LemonUI.Menus;
using LemonUI;
using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using GTA.Native;
using GTA.UI;
using System.Collections.Generic;
using GTA.Math;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using static RageTrainerMenu;




public class RageTrainerMenu : Script


{
    private readonly ObjectPool pool = new ObjectPool();
    private readonly NativeMenu mainMenu = new NativeMenu("Rage Trainer", "Main Menu");
    private readonly NativeMenu localSubMenu = new NativeMenu("Rage Trainer", "Local");
    private readonly NativeMenu weaponsSubMenu = new NativeMenu("Rage Trainer", "Weapons");
    private readonly NativeMenu vehiclesSubMenu = new NativeMenu("Rage Trainer", "Vehicles");
    private readonly NativeMenu spawnerSubMenu = new NativeMenu("Rage Trainer", "Spawner");
    private readonly NativeMenu spawn_vehicleSubMenu = new NativeMenu("Rage Trainer", "Vehicles");
    private readonly NativeMenu boatsSubMenu = new NativeMenu("Rage Trainer", "Boats");
    private readonly NativeMenu commercialSubMenu = new NativeMenu("Rage Trainer", "Commercial");
    private readonly NativeMenu compactSubMenu = new NativeMenu("Rage Trainer", "Compact");
    private readonly NativeMenu coupeSubMenu = new NativeMenu("Rage Trainer", "Coupe");
    private readonly NativeMenu cycleSubMenu = new NativeMenu("Rage Trainer", "Cycle");
    private readonly NativeMenu emergencySubMenu = new NativeMenu("Rage Trainer", "Emergency");
    private readonly NativeMenu helicopterSubMenu = new NativeMenu("Rage Trainer", "Helicopter");
    private readonly NativeMenu industrialSubMenu = new NativeMenu("Rage Trainer", "Industrial");
    private readonly NativeMenu militarySubMenu = new NativeMenu("Rage Trainer", "Military");
    private readonly NativeMenu motorcycleSubMenu = new NativeMenu("Rage Trainer", "Motorcycle");
    private readonly NativeMenu muscleSubMenu = new NativeMenu("Rage Trainer", "Muscle");
    private readonly NativeMenu offRoadSubMenu = new NativeMenu("Rage Trainer", "Off-Road");
    private readonly NativeMenu openWheelSubMenu = new NativeMenu("Rage Trainer", "Open Wheel");
    private readonly NativeMenu planeSubMenu = new NativeMenu("Rage Trainer", "Plane");
    private readonly NativeMenu railSubMenu = new NativeMenu("Rage Trainer", "Rail");
    private readonly NativeMenu sedanSubMenu = new NativeMenu("Rage Trainer", "Sedan");
    private readonly NativeMenu serviceSubMenu = new NativeMenu("Rage Trainer", "Service");
    private readonly NativeMenu sportsSubMenu = new NativeMenu("Rage Trainer", "Sports");
    private readonly NativeMenu sportsClassicSubMenu = new NativeMenu("Rage Trainer", "Sport Classic");
    private readonly NativeMenu superSubMenu = new NativeMenu("Rage Trainer", "Super");
    private readonly NativeMenu suvSubMenu = new NativeMenu("Rage Trainer", "SUV");
    private readonly NativeMenu utilitySubMenu = new NativeMenu("Rage Trainer", "Utility");
    private readonly NativeMenu vanSubMenu = new NativeMenu("Rage Trainer", "Vans");
    private readonly NativeMenu othersSubMenu = new NativeMenu("Rage Trainer", "Others");
    private readonly NativeMenu teleportSubMenu = new NativeMenu("Rage Trainer", "Teleport");
    private readonly NativeMenu recoverySubMenu = new NativeMenu("Rage Trainer", "Recovery");
    private readonly NativeMenu moneySubMenu = new NativeMenu("Rage Trainer", "Money");
    private readonly NativeMenu addSubMenu = new NativeMenu("Rage Trainer", "Add");
    private readonly NativeMenu removeSubMenu = new NativeMenu("Rage Trainer", "Remove");
    private readonly NativeMenu worldSubMenu = new NativeMenu("Rage Trainer", "World");
    private readonly NativeMenu timeSubMenu = new NativeMenu("Rage Trainer", "Time");
    private readonly NativeMenu weatherSubMenu = new NativeMenu("Rage Trainer", "Weather");
    private readonly NativeMenu miscSubMenu = new NativeMenu("Rage Trainer", "Misc");
    private readonly NativeMenu settingsSubMenu = new NativeMenu("Rage Trainer", "Settings");
    private readonly NativeMenu disableSubMenu = new NativeMenu("Rage Trainer", "Disable");


    private int menuAlignmentOption = 1;
    private bool godModeEnabled = false;
    private bool lawlessEnabled = false;
    private bool noRagdollEnabled = false;
    private bool everyoneIgnoreEnabled = false;
    private bool invisibilityEnabled = false;
    private bool infiniteStaminaEnabled = false;
    private bool infiniteSpecialAbilityEnabled = false;
    private bool superPunchEnabled = false;
    private bool noReloadEnabled = false;
    private bool oneHitKillEnabled = false;
    private int currentTintIndex = 0;
    private bool rainbowGunEnabled = false;
    private int rainbowGunTickCounter = 0;
    private const int RainbowGunTickDelay = 5;
    private bool vehicleGodMode = false;
    private bool seatbeltEnabled = false;
    private bool isMenuOpen = false;
    private bool isSubmenuOpen = false;
    private DateTime lastToggleTime = DateTime.MinValue;
    private const int MenuToggleCooldownMilliseconds = 300;

    public RageTrainerMenu()
    {
        Notification.Show("~r~Rage~s~ Trainer V1.1 Loaded!\nPress F3 To Open Menu.");




        // Local

        NativeCheckboxItem godCheckbox = new NativeCheckboxItem("God", "Become A God", false);
        godCheckbox.CheckboxChanged += (sender, e) => GodToggle = godCheckbox.Checked;
        localSubMenu.Add(godCheckbox);


        NativeCheckboxItem lawlessCheckbox = new NativeCheckboxItem("Lawless", "Never Get Wanted", false);
        lawlessCheckbox.CheckboxChanged += (sender, e) => LawlessToggle = lawlessCheckbox.Checked;
        localSubMenu.Add(lawlessCheckbox);

        NativeCheckboxItem everyoneIgnoreCheckbox = new NativeCheckboxItem("Everyone Ignore", false);
        everyoneIgnoreCheckbox.CheckboxChanged += EveryoneIgnoreToggle_CheckboxChanged;
        localSubMenu.Add(everyoneIgnoreCheckbox);

        NativeCheckboxItem invisibilityCheckbox = new NativeCheckboxItem("Invisibility", false);
        invisibilityCheckbox.CheckboxChanged += (sender, e) => ToggleInvisibility();
        localSubMenu.Add(invisibilityCheckbox);

        NativeCheckboxItem infiniteStaminaCheckbox = new NativeCheckboxItem("Infinite Stamina", false);
        infiniteStaminaCheckbox.CheckboxChanged += (sender, e) => ToggleInfiniteStamina();
        localSubMenu.Add(infiniteStaminaCheckbox);

        NativeCheckboxItem infiniteSpecialAbilityCheckbox = new NativeCheckboxItem("Infinite Special Ability", false);
        infiniteSpecialAbilityCheckbox.CheckboxChanged += (sender, e) => InfiniteSpecialAbilityToggle = infiniteSpecialAbilityCheckbox.Checked;
        localSubMenu.Add(infiniteSpecialAbilityCheckbox);

        NativeItem gainWantedLevelItem = new NativeItem("Gain Wanted Level");
        gainWantedLevelItem.Activated += (sender, e) => GainWantedLevel();
        localSubMenu.Add(gainWantedLevelItem);

        NativeItem removeWantedLevelItem = new NativeItem("Remove Wanted Level");
        removeWantedLevelItem.Activated += (sender, e) => RemoveWantedLevel();
        localSubMenu.Add(removeWantedLevelItem);


        NativeItem healPlayerItem = new NativeItem("Heal Player", "Fully Restore Health And Armor");
        healPlayerItem.Activated += (sender, e) => HealPlayerAction_Activated(sender, e);
        localSubMenu.Add(healPlayerItem);


        NativeItem cleanPlayerItem = new NativeItem("Clean Player");
        cleanPlayerItem.Activated += (sender, e) => CleanPlayerAction_Activated(sender, e);
        localSubMenu.Add(cleanPlayerItem);

        NativeItem suicideItem = new NativeItem("~r~Suicide~s~");
        suicideItem.Activated += (sender, e) => Suicide();
        localSubMenu.Add(suicideItem);


        // Weapons 

        NativeItem giveAllWeaponsItem = new NativeItem("Give All Weapons");
        giveAllWeaponsItem.Activated += (sender, e) => GiveAllWeapons();
        weaponsSubMenu.Add(giveAllWeaponsItem);

        NativeItem removeAllWeaponsItem = new NativeItem("Remove All Weapons");
        removeAllWeaponsItem.Activated += (sender, e) => Game.Player.Character.Weapons.RemoveAll();
        weaponsSubMenu.Add(removeAllWeaponsItem);


        NativeItem maxAmmoItem = new NativeItem("Max Ammo");
        maxAmmoItem.Activated += (sender, e) => MaxAmmo();
        weaponsSubMenu.Add(maxAmmoItem);


        NativeCheckboxItem infiniteAmmoCheckbox = new NativeCheckboxItem("Infinite Ammo", false);
        infiniteAmmoCheckbox.CheckboxChanged += (sender, e) => InfiniteAmmoToggle_CheckboxChanged(sender, e);
        weaponsSubMenu.Add(infiniteAmmoCheckbox);

        NativeCheckboxItem noReloadCheckbox = new NativeCheckboxItem("No Reload", false);
        noReloadCheckbox.CheckboxChanged += NoReloadToggle_CheckboxChanged;
        weaponsSubMenu.Add(noReloadCheckbox);


        NativeCheckboxItem rainbowGunCheckbox = new NativeCheckboxItem("Rainbow", "Cycle Through All Gun Tints", false);
        rainbowGunCheckbox.CheckboxChanged += (sender, e) => RainbowGunToggle = rainbowGunCheckbox.Checked;
        weaponsSubMenu.Add(rainbowGunCheckbox);

        // Vehicles

        NativeCheckboxItem vehicleGodModeCheckbox = new NativeCheckboxItem("God", false);
        vehicleGodModeCheckbox.CheckboxChanged += VehicleGodModeToggle_CheckboxChanged;
        vehiclesSubMenu.Add(vehicleGodModeCheckbox);

       


        NativeItem fixVehicleItem = new NativeItem("Fix Vehicle");
        fixVehicleItem.Activated += (sender, e) => FixVehicle();
        vehiclesSubMenu.Add(fixVehicleItem);

        NativeItem cleanVehicleItem = new NativeItem("Clean Vehicle");
        cleanVehicleItem.Activated += (sender, e) => CleanVehicle();
        vehiclesSubMenu.Add(cleanVehicleItem);

        NativeItem applyDirtVehicleItem = new NativeItem("Apply Dirt");
        applyDirtVehicleItem.Activated += (sender, e) => ApplyDirtVehicle();
        vehiclesSubMenu.Add(applyDirtVehicleItem);

        // Spawner

        // Boats

        NativeItem avisaItem = new NativeItem("Avisa");
        avisaItem.Activated += (sender, e) => SpawnVehicle("avisa");
        boatsSubMenu.Add(avisaItem);

        NativeItem dinghyItem = new NativeItem("Dinghy");
        dinghyItem.Activated += (sender, e) => SpawnVehicle("Dinghy");
        boatsSubMenu.Add(dinghyItem);

        NativeItem dinghy2Item = new NativeItem("Dinghy 2");
        dinghy2Item.Activated += (sender, e) => SpawnVehicle("dinghy2");
        boatsSubMenu.Add(dinghy2Item);

        NativeItem dinghy3Item = new NativeItem("Dinghy 3");
        dinghy3Item.Activated += (sender, e) => SpawnVehicle("dinghy3");
        boatsSubMenu.Add(dinghy3Item);

        NativeItem dinghy4Item = new NativeItem("Dinghy 4");
        dinghy4Item.Activated += (sender, e) => SpawnVehicle("dinghy4");
        boatsSubMenu.Add(dinghy4Item);

        NativeItem dinghy5Item = new NativeItem("Weaponized Dinghy");
        dinghy5Item.Activated += (sender, e) => SpawnVehicle("dinghy5");
        boatsSubMenu.Add(dinghy5Item);

        NativeItem jetmaxItem = new NativeItem("Jetmax");
        jetmaxItem.Activated += (sender, e) => SpawnVehicle("jetmax");
        boatsSubMenu.Add(jetmaxItem);

        NativeItem kosatkaItem = new NativeItem("Kosatka");
        kosatkaItem.Activated += (sender, e) => SpawnVehicle("kosatka");
        boatsSubMenu.Add(kosatkaItem);

        NativeItem longfinItem = new NativeItem("Longfin");
        longfinItem.Activated += (sender, e) => SpawnVehicle("longfin");
        boatsSubMenu.Add(longfinItem);

        NativeItem marquisItem = new NativeItem("Marquis");
        marquisItem.Activated += (sender, e) => SpawnVehicle("marquis");
        boatsSubMenu.Add(marquisItem);

        NativeItem patrolboatItem = new NativeItem("Patrol Boat");
        patrolboatItem.Activated += (sender, e) => SpawnVehicle("patrolboat");
        boatsSubMenu.Add(patrolboatItem);

        NativeItem predatorItem = new NativeItem("Police Predator");
        predatorItem.Activated += (sender, e) => SpawnVehicle("Predator");
        boatsSubMenu.Add(predatorItem);

        NativeItem seasharkItem = new NativeItem("Seashark");
        seasharkItem.Activated += (sender, e) => SpawnVehicle("seashark");
        boatsSubMenu.Add(seasharkItem);

        NativeItem seashark2Item = new NativeItem("Seashark 2");
        seashark2Item.Activated += (sender, e) => SpawnVehicle("seashark2");
        boatsSubMenu.Add(seashark2Item);

        NativeItem seashark3Item = new NativeItem("Seashark 3");
        seashark3Item.Activated += (sender, e) => SpawnVehicle("seashark3");
        boatsSubMenu.Add(seashark3Item);

        NativeItem speederItem = new NativeItem("Speeder");
        speederItem.Activated += (sender, e) => SpawnVehicle("speeder");
        boatsSubMenu.Add(speederItem);

        NativeItem speeder2Item = new NativeItem("Speeder 2");
        speeder2Item.Activated += (sender, e) => SpawnVehicle("speeder2");
        boatsSubMenu.Add(speeder2Item);

        NativeItem squaloItem = new NativeItem("Squalo");
        squaloItem.Activated += (sender, e) => SpawnVehicle("squalo");
        boatsSubMenu.Add(squaloItem);

        NativeItem submersibleItem = new NativeItem("Submersible");
        submersibleItem.Activated += (sender, e) => SpawnVehicle("submersible");
        boatsSubMenu.Add(submersibleItem);

        NativeItem submersible2Item = new NativeItem("Kraken");
        submersible2Item.Activated += (sender, e) => SpawnVehicle("submersible2");
        boatsSubMenu.Add(submersible2Item);

        NativeItem suntrapItem = new NativeItem("Suntrap");
        suntrapItem.Activated += (sender, e) => SpawnVehicle("Suntrap");
        boatsSubMenu.Add(suntrapItem);

        NativeItem toroItem = new NativeItem("Toro");
        toroItem.Activated += (sender, e) => SpawnVehicle("toro");
        boatsSubMenu.Add(toroItem);

        NativeItem toro2Item = new NativeItem("Toro 2");
        toro2Item.Activated += (sender, e) => SpawnVehicle("toro2");
        boatsSubMenu.Add(toro2Item);

        NativeItem tropicItem = new NativeItem("Tropic");
        tropicItem.Activated += (sender, e) => SpawnVehicle("tropic");
        boatsSubMenu.Add(tropicItem);

        NativeItem tropic2Item = new NativeItem("Tropic 2");
        tropic2Item.Activated += (sender, e) => SpawnVehicle("tropic2");
        boatsSubMenu.Add(tropic2Item);

        NativeItem tugItem = new NativeItem("Tug");
        tugItem.Activated += (sender, e) => SpawnVehicle("tug");
        boatsSubMenu.Add(tugItem);

        // Commercial

        NativeItem bensonItem = new NativeItem("Benson");
        bensonItem.Activated += (sender, e) => SpawnVehicle("Benson");
        commercialSubMenu.Add(bensonItem);

        NativeItem biffItem = new NativeItem("Biff");
        biffItem.Activated += (sender, e) => SpawnVehicle("Biff");
        commercialSubMenu.Add(biffItem);

        NativeItem cerberusItem = new NativeItem("Cerberus");
        cerberusItem.Activated += (sender, e) => SpawnVehicle("cerberus");
        commercialSubMenu.Add(cerberusItem);

        NativeItem cerberus2Item = new NativeItem("Cerberus 2");
        cerberus2Item.Activated += (sender, e) => SpawnVehicle("cerberus2");
        commercialSubMenu.Add(cerberus2Item);

        NativeItem cerberus3Item = new NativeItem("Cerberus 3");
        cerberus3Item.Activated += (sender, e) => SpawnVehicle("cerberus3");
        commercialSubMenu.Add(cerberus3Item);

        NativeItem haulerItem = new NativeItem("Hauler");
        haulerItem.Activated += (sender, e) => SpawnVehicle("Hauler");
        commercialSubMenu.Add(haulerItem);

        NativeItem hauler2Item = new NativeItem("Hauler 2");
        hauler2Item.Activated += (sender, e) => SpawnVehicle("Hauler2");
        commercialSubMenu.Add(hauler2Item);

        NativeItem muleItem = new NativeItem("Mule");
        muleItem.Activated += (sender, e) => SpawnVehicle("Mule");
        commercialSubMenu.Add(muleItem);

        NativeItem mule2Item = new NativeItem("Mule 2");
        mule2Item.Activated += (sender, e) => SpawnVehicle("Mule2");
        commercialSubMenu.Add(mule2Item);

        NativeItem mule3Item = new NativeItem("Mule 3");
        mule3Item.Activated += (sender, e) => SpawnVehicle("Mule3");
        commercialSubMenu.Add(mule3Item);

        NativeItem mule4Item = new NativeItem("Mule 4");
        mule4Item.Activated += (sender, e) => SpawnVehicle("mule4");
        commercialSubMenu.Add(mule4Item);

        NativeItem mule5Item = new NativeItem("Mule 5");
        mule5Item.Activated += (sender, e) => SpawnVehicle("mule5");
        commercialSubMenu.Add(mule5Item);

        NativeItem packerItem = new NativeItem("Packer");
        packerItem.Activated += (sender, e) => SpawnVehicle("Packer");
        commercialSubMenu.Add(packerItem);

        NativeItem phantomItem = new NativeItem("Phantom");
        phantomItem.Activated += (sender, e) => SpawnVehicle("Phantom");
        commercialSubMenu.Add(phantomItem);

        NativeItem phantom2Item = new NativeItem("Phantom 2");
        phantom2Item.Activated += (sender, e) => SpawnVehicle("phantom2");
        commercialSubMenu.Add(phantom2Item);

        NativeItem phantom3Item = new NativeItem("Phantom 3");
        phantom3Item.Activated += (sender, e) => SpawnVehicle("phantom3");
        commercialSubMenu.Add(phantom3Item);

        NativeItem pounderItem = new NativeItem("Pounder");
        pounderItem.Activated += (sender, e) => SpawnVehicle("Pounder");
        commercialSubMenu.Add(pounderItem);

        NativeItem pounder2Item = new NativeItem("Pounder 2");
        pounder2Item.Activated += (sender, e) => SpawnVehicle("pounder2");
        commercialSubMenu.Add(pounder2Item);

        NativeItem stockadeItem = new NativeItem("Stockade");
        stockadeItem.Activated += (sender, e) => SpawnVehicle("stockade");
        commercialSubMenu.Add(stockadeItem);

        NativeItem stockade3Item = new NativeItem("Stockade 3");
        stockade3Item.Activated += (sender, e) => SpawnVehicle("stockade3");
        commercialSubMenu.Add(stockade3Item);

        NativeItem terbyteItem = new NativeItem("Terrorbyte");
        terbyteItem.Activated += (sender, e) => SpawnVehicle("terbyte");
        commercialSubMenu.Add(terbyteItem);

        // Compact

        NativeItem asboItem = new NativeItem("Asbo");
        asboItem.Activated += (sender, e) => SpawnVehicle("Asbo");
        compactSubMenu.Add(asboItem);

        NativeItem blistaItem = new NativeItem("Blista");
        blistaItem.Activated += (sender, e) => SpawnVehicle("Blista");
        compactSubMenu.Add(blistaItem);

        NativeItem briosoItem = new NativeItem("Brioso");
        briosoItem.Activated += (sender, e) => SpawnVehicle("Brioso");
        compactSubMenu.Add(briosoItem);

        NativeItem brioso2Item = new NativeItem("Brioso 2");
        brioso2Item.Activated += (sender, e) => SpawnVehicle("Brioso2");
        compactSubMenu.Add(brioso2Item);

        NativeItem brioso3Item = new NativeItem("Brioso 3");
        brioso3Item.Activated += (sender, e) => SpawnVehicle("Brioso3");
        compactSubMenu.Add(brioso3Item);

        NativeItem clubItem = new NativeItem("Club");
        clubItem.Activated += (sender, e) => SpawnVehicle("Club");
        compactSubMenu.Add(clubItem);

        NativeItem dilettanteItem = new NativeItem("Dilettante");
        dilettanteItem.Activated += (sender, e) => SpawnVehicle("Dilettante");
        compactSubMenu.Add(dilettanteItem);

        NativeItem dilettante2Item = new NativeItem("Dilettante 2");
        dilettante2Item.Activated += (sender, e) => SpawnVehicle("Dilettante2");
        compactSubMenu.Add(dilettante2Item);

        NativeItem issi2Item = new NativeItem("Issi 2");
        issi2Item.Activated += (sender, e) => SpawnVehicle("Issi2");
        compactSubMenu.Add(issi2Item);

        NativeItem issi3Item = new NativeItem("Issi 3");
        issi3Item.Activated += (sender, e) => SpawnVehicle("Issi3");
        compactSubMenu.Add(issi3Item);

        NativeItem issi4Item = new NativeItem("Issi 4");
        issi4Item.Activated += (sender, e) => SpawnVehicle("Issi4");
        compactSubMenu.Add(issi4Item);

        NativeItem issi5Item = new NativeItem("Issi 5");
        issi5Item.Activated += (sender, e) => SpawnVehicle("Issi5");
        compactSubMenu.Add(issi5Item);

        NativeItem issi6Item = new NativeItem("Issi 6");
        issi6Item.Activated += (sender, e) => SpawnVehicle("Issi6");
        compactSubMenu.Add(issi6Item);

        NativeItem kanjoItem = new NativeItem("Kanjo");
        kanjoItem.Activated += (sender, e) => SpawnVehicle("Kanjo");
        compactSubMenu.Add(kanjoItem);

        NativeItem pantoItem = new NativeItem("Panto");
        pantoItem.Activated += (sender, e) => SpawnVehicle("Panto");
        compactSubMenu.Add(pantoItem);

        NativeItem prairieItem = new NativeItem("Prairie");
        prairieItem.Activated += (sender, e) => SpawnVehicle("Prairie");
        compactSubMenu.Add(prairieItem);

        NativeItem rhapsodyItem = new NativeItem("Rhapsody");
        rhapsodyItem.Activated += (sender, e) => SpawnVehicle("Rhapsody");
        compactSubMenu.Add(rhapsodyItem);

        NativeItem weevilItem = new NativeItem("Weevil");
        weevilItem.Activated += (sender, e) => SpawnVehicle("Weevil");
        compactSubMenu.Add(weevilItem);

        // Coupe

        NativeItem cogcabrioItem = new NativeItem("Cognoscenti Cabrio");
        cogcabrioItem.Activated += (sender, e) => SpawnVehicle("cogcabrio");
        coupeSubMenu.Add(cogcabrioItem);

        NativeItem exemplarItem = new NativeItem("Exemplar");
        exemplarItem.Activated += (sender, e) => SpawnVehicle("exemplar");
        coupeSubMenu.Add(exemplarItem);

        NativeItem f620Item = new NativeItem("F620");
        f620Item.Activated += (sender, e) => SpawnVehicle("f620");
        coupeSubMenu.Add(f620Item);

        NativeItem felonItem = new NativeItem("Felon");
        felonItem.Activated += (sender, e) => SpawnVehicle("felon");
        coupeSubMenu.Add(felonItem);

        NativeItem felon2Item = new NativeItem("Felon GT");
        felon2Item.Activated += (sender, e) => SpawnVehicle("felon2");
        coupeSubMenu.Add(felon2Item);

        NativeItem jackalItem = new NativeItem("Jackal");
        jackalItem.Activated += (sender, e) => SpawnVehicle("jackal");
        coupeSubMenu.Add(jackalItem);

        NativeItem kanjosjItem = new NativeItem("Kanjo SJ");
        kanjosjItem.Activated += (sender, e) => SpawnVehicle("kanjosj");
        coupeSubMenu.Add(kanjosjItem);

        NativeItem oracleItem = new NativeItem("Oracle XS");
        oracleItem.Activated += (sender, e) => SpawnVehicle("oracle");
        coupeSubMenu.Add(oracleItem);

        NativeItem oracle2Item = new NativeItem("Oracle");
        oracle2Item.Activated += (sender, e) => SpawnVehicle("oracle2");
        coupeSubMenu.Add(oracle2Item);

        NativeItem postludeItem = new NativeItem("Postlude");
        postludeItem.Activated += (sender, e) => SpawnVehicle("postlude");
        coupeSubMenu.Add(postludeItem);

        NativeItem previonItem = new NativeItem("Previon");
        previonItem.Activated += (sender, e) => SpawnVehicle("previon");
        coupeSubMenu.Add(previonItem);

        NativeItem sentinelItem = new NativeItem("Sentinel XS");
        sentinelItem.Activated += (sender, e) => SpawnVehicle("sentinel");
        coupeSubMenu.Add(sentinelItem);

        NativeItem sentinel2Item = new NativeItem("Sentinel");
        sentinel2Item.Activated += (sender, e) => SpawnVehicle("sentinel2");
        coupeSubMenu.Add(sentinel2Item);

        NativeItem windsorItem = new NativeItem("Windsor");
        windsorItem.Activated += (sender, e) => SpawnVehicle("windsor");
        coupeSubMenu.Add(windsorItem);

        NativeItem windsor2Item = new NativeItem("Windsor Drop");
        windsor2Item.Activated += (sender, e) => SpawnVehicle("windsor2");
        coupeSubMenu.Add(windsor2Item);

        NativeItem zionItem = new NativeItem("Zion");
        zionItem.Activated += (sender, e) => SpawnVehicle("zion");
        coupeSubMenu.Add(zionItem);

        NativeItem zion2Item = new NativeItem("Zion Cabrio");
        zion2Item.Activated += (sender, e) => SpawnVehicle("zion2");
        coupeSubMenu.Add(zion2Item);

        // Cycle

        NativeItem bmxItem = new NativeItem("BMX");
        bmxItem.Activated += (sender, e) => SpawnVehicle("BMX");
        cycleSubMenu.Add(bmxItem);

        NativeItem cruiserItem = new NativeItem("Cruiser");
        cruiserItem.Activated += (sender, e) => SpawnVehicle("cruiser");
        cycleSubMenu.Add(cruiserItem);

        NativeItem fixterItem = new NativeItem("Fixter");
        fixterItem.Activated += (sender, e) => SpawnVehicle("fixter");
        cycleSubMenu.Add(fixterItem);

        NativeItem scorcherItem = new NativeItem("Scorcher");
        scorcherItem.Activated += (sender, e) => SpawnVehicle("scorcher");
        cycleSubMenu.Add(scorcherItem);

        NativeItem tribikeItem = new NativeItem("Whippet Race Bike");
        tribikeItem.Activated += (sender, e) => SpawnVehicle("tribike");
        cycleSubMenu.Add(tribikeItem);

        NativeItem tribike2Item = new NativeItem("Endurex Race Bike");
        tribike2Item.Activated += (sender, e) => SpawnVehicle("tribike2");
        cycleSubMenu.Add(tribike2Item);

        NativeItem tribike3Item = new NativeItem("Tri-Cycles Race Bike");
        tribike3Item.Activated += (sender, e) => SpawnVehicle("tribike3");
        cycleSubMenu.Add(tribike3Item);

        // Emergency

        NativeItem ambulanceItem = new NativeItem("Ambulance");
        ambulanceItem.Activated += (sender, e) => SpawnVehicle("ambulance");
        emergencySubMenu.Add(ambulanceItem);

        NativeItem fbiItem = new NativeItem("FIB");
        fbiItem.Activated += (sender, e) => SpawnVehicle("FBI");
        emergencySubMenu.Add(fbiItem);

        NativeItem fbi2Item = new NativeItem("FIB");
        fbi2Item.Activated += (sender, e) => SpawnVehicle("FBI2");
        emergencySubMenu.Add(fbi2Item);

        NativeItem fireTruckItem = new NativeItem("Fire Truck");
        fireTruckItem.Activated += (sender, e) => SpawnVehicle("firetruk");
        emergencySubMenu.Add(fireTruckItem);

        NativeItem lifeguardItem = new NativeItem("Lifeguard");
        lifeguardItem.Activated += (sender, e) => SpawnVehicle("lguard");
        emergencySubMenu.Add(lifeguardItem);

        NativeItem policePrisonBusItem = new NativeItem("Police Prison Bus");
        policePrisonBusItem.Activated += (sender, e) => SpawnVehicle("pbus");
        emergencySubMenu.Add(policePrisonBusItem);

        NativeItem policeCruiserItem = new NativeItem("Police Cruiser");
        policeCruiserItem.Activated += (sender, e) => SpawnVehicle("police");
        emergencySubMenu.Add(policeCruiserItem);

        NativeItem policeCruiser2Item = new NativeItem("Police Cruiser");
        policeCruiser2Item.Activated += (sender, e) => SpawnVehicle("police2");
        emergencySubMenu.Add(policeCruiser2Item);

        NativeItem policeCruiser3Item = new NativeItem("Police Cruiser");
        policeCruiser3Item.Activated += (sender, e) => SpawnVehicle("police3");
        emergencySubMenu.Add(policeCruiser3Item);

        NativeItem unmarkedCruiserItem = new NativeItem("Unmarked Cruiser");
        unmarkedCruiserItem.Activated += (sender, e) => SpawnVehicle("police4");
        emergencySubMenu.Add(unmarkedCruiserItem);

        NativeItem policeBikeItem = new NativeItem("Police Bike");
        policeBikeItem.Activated += (sender, e) => SpawnVehicle("policeb");
        emergencySubMenu.Add(policeBikeItem);

        NativeItem policeRancherItem = new NativeItem("Police Rancher");
        policeRancherItem.Activated += (sender, e) => SpawnVehicle("policeold1");
        emergencySubMenu.Add(policeRancherItem);

        NativeItem policeRoadcruiserItem = new NativeItem("Police Roadcruiser");
        policeRoadcruiserItem.Activated += (sender, e) => SpawnVehicle("policeold2");
        emergencySubMenu.Add(policeRoadcruiserItem);

        NativeItem policeTransporterItem = new NativeItem("Police Transporter");
        policeTransporterItem.Activated += (sender, e) => SpawnVehicle("policet");
        emergencySubMenu.Add(policeTransporterItem);

        NativeItem parkRangerItem = new NativeItem("Park Ranger");
        parkRangerItem.Activated += (sender, e) => SpawnVehicle("pRanger");
        emergencySubMenu.Add(parkRangerItem);

        NativeItem policeRiotItem = new NativeItem("Police Riot");
        policeRiotItem.Activated += (sender, e) => SpawnVehicle("RIOT");
        emergencySubMenu.Add(policeRiotItem);

        NativeItem rcvItem = new NativeItem("RCV");
        rcvItem.Activated += (sender, e) => SpawnVehicle("riot2");
        emergencySubMenu.Add(rcvItem);

        NativeItem sheriffCruiserItem = new NativeItem("Sheriff Cruiser");
        sheriffCruiserItem.Activated += (sender, e) => SpawnVehicle("SHERIFF");
        emergencySubMenu.Add(sheriffCruiserItem);

        NativeItem sheriffSUVItem = new NativeItem("Sheriff SUV");
        sheriffSUVItem.Activated += (sender, e) => SpawnVehicle("sheriff2");
        emergencySubMenu.Add(sheriffSUVItem);

        // Helicopter 

        NativeItem akulaItem = new NativeItem("Akula");
        akulaItem.Activated += (sender, e) => SpawnVehicle("akula");
        helicopterSubMenu.Add(akulaItem);

        NativeItem annihilatorItem = new NativeItem("Annihilator");
        annihilatorItem.Activated += (sender, e) => SpawnVehicle("annihilator");
        helicopterSubMenu.Add(annihilatorItem);

        NativeItem annihilator2Item = new NativeItem("Annihilator Stealth");
        annihilator2Item.Activated += (sender, e) => SpawnVehicle("annihilator2");
        helicopterSubMenu.Add(annihilator2Item);

        NativeItem buzzardItem = new NativeItem("Buzzard Attack Chopper");
        buzzardItem.Activated += (sender, e) => SpawnVehicle("buzzard");
        helicopterSubMenu.Add(buzzardItem);

        NativeItem buzzard2Item = new NativeItem("Buzzard");
        buzzard2Item.Activated += (sender, e) => SpawnVehicle("Buzzard2");
        helicopterSubMenu.Add(buzzard2Item);

        NativeItem cargobobItem = new NativeItem("Cargobob");
        cargobobItem.Activated += (sender, e) => SpawnVehicle("Cargobob");
        helicopterSubMenu.Add(cargobobItem);

        NativeItem cargobob2Item = new NativeItem("Cargobob");
        cargobob2Item.Activated += (sender, e) => SpawnVehicle("cargobob2");
        helicopterSubMenu.Add(cargobob2Item);

        NativeItem cargobob3Item = new NativeItem("Cargobob");
        cargobob3Item.Activated += (sender, e) => SpawnVehicle("Cargobob3");
        helicopterSubMenu.Add(cargobob3Item);

        NativeItem cargobob4Item = new NativeItem("Cargobob");
        cargobob4Item.Activated += (sender, e) => SpawnVehicle("Cargobob4");
        helicopterSubMenu.Add(cargobob4Item);

        NativeItem conadaItem = new NativeItem("Conada");
        conadaItem.Activated += (sender, e) => SpawnVehicle("conada");
        helicopterSubMenu.Add(conadaItem);

        NativeItem froggerItem = new NativeItem("Frogger");
        froggerItem.Activated += (sender, e) => SpawnVehicle("Frogger");
        helicopterSubMenu.Add(froggerItem);

        NativeItem frogger2Item = new NativeItem("Frogger");
        frogger2Item.Activated += (sender, e) => SpawnVehicle("frogger2");
        helicopterSubMenu.Add(frogger2Item);

        NativeItem havokItem = new NativeItem("Havok");
        havokItem.Activated += (sender, e) => SpawnVehicle("havok");
        helicopterSubMenu.Add(havokItem);

        NativeItem hunterItem = new NativeItem("FH-1 Hunter");
        hunterItem.Activated += (sender, e) => SpawnVehicle("hunter");
        helicopterSubMenu.Add(hunterItem);

        NativeItem maverickItem = new NativeItem("Maverick");
        maverickItem.Activated += (sender, e) => SpawnVehicle("maverick");
        helicopterSubMenu.Add(maverickItem);

        NativeItem polmavItem = new NativeItem("Police Maverick");
        polmavItem.Activated += (sender, e) => SpawnVehicle("polmav");
        helicopterSubMenu.Add(polmavItem);

        NativeItem savageItem = new NativeItem("Savage");
        savageItem.Activated += (sender, e) => SpawnVehicle("savage");
        helicopterSubMenu.Add(savageItem);

        NativeItem seasparrowItem = new NativeItem("Sea Sparrow");
        seasparrowItem.Activated += (sender, e) => SpawnVehicle("seasparrow");
        helicopterSubMenu.Add(seasparrowItem);

        NativeItem seasparrow2Item = new NativeItem("Sparrow");
        seasparrow2Item.Activated += (sender, e) => SpawnVehicle("seasparrow2");
        helicopterSubMenu.Add(seasparrow2Item);

        NativeItem seasparrow3Item = new NativeItem("Sparrow");
        seasparrow3Item.Activated += (sender, e) => SpawnVehicle("seasparrow3");
        helicopterSubMenu.Add(seasparrow3Item);

        NativeItem skyliftItem = new NativeItem("Skylift");
        skyliftItem.Activated += (sender, e) => SpawnVehicle("skylift");
        helicopterSubMenu.Add(skyliftItem);

        NativeItem supervolitoItem = new NativeItem("SuperVolito");
        supervolitoItem.Activated += (sender, e) => SpawnVehicle("supervolito");
        helicopterSubMenu.Add(supervolitoItem);

        NativeItem supervolito2Item = new NativeItem("SuperVolito Carbon");
        supervolito2Item.Activated += (sender, e) => SpawnVehicle("supervolito2");
        helicopterSubMenu.Add(supervolito2Item);

        NativeItem swiftItem = new NativeItem("Swift");
        swiftItem.Activated += (sender, e) => SpawnVehicle("swift");
        helicopterSubMenu.Add(swiftItem);

        NativeItem swift2Item = new NativeItem("Swift Deluxe");
        swift2Item.Activated += (sender, e) => SpawnVehicle("swift2");
        helicopterSubMenu.Add(swift2Item);

        NativeItem valkyrieItem = new NativeItem("Valkyrie");
        valkyrieItem.Activated += (sender, e) => SpawnVehicle("valkyrie");
        helicopterSubMenu.Add(valkyrieItem);

        NativeItem valkyrie2Item = new NativeItem("Valkyrie MOD.0");
        valkyrie2Item.Activated += (sender, e) => SpawnVehicle("valkyrie2");
        helicopterSubMenu.Add(valkyrie2Item);

        NativeItem volatusItem = new NativeItem("Volatus");
        volatusItem.Activated += (sender, e) => SpawnVehicle("volatus");
        helicopterSubMenu.Add(volatusItem);

        // Industrial

        NativeItem bulldozerItem = new NativeItem("Bulldozer");
        bulldozerItem.Activated += (sender, e) => SpawnVehicle("Bulldozer");
        industrialSubMenu.Add(bulldozerItem);

        NativeItem cutterItem = new NativeItem("Cutter");
        cutterItem.Activated += (sender, e) => SpawnVehicle("Cutter");
        industrialSubMenu.Add(cutterItem);

        NativeItem dumpItem = new NativeItem("Dump");
        dumpItem.Activated += (sender, e) => SpawnVehicle("Dump");
        industrialSubMenu.Add(dumpItem);

        NativeItem flatbedItem = new NativeItem("Flatbed");
        flatbedItem.Activated += (sender, e) => SpawnVehicle("Flatbed");
        industrialSubMenu.Add(flatbedItem);

        NativeItem guardianItem = new NativeItem("Guardian");
        guardianItem.Activated += (sender, e) => SpawnVehicle("Guardian");
        industrialSubMenu.Add(guardianItem);

        NativeItem handlerItem = new NativeItem("Dock Handler");
        handlerItem.Activated += (sender, e) => SpawnVehicle("Dock Handler");
        industrialSubMenu.Add(handlerItem);

        NativeItem mixerItem = new NativeItem("Mixer");
        mixerItem.Activated += (sender, e) => SpawnVehicle("Mixer");
        industrialSubMenu.Add(mixerItem);

        NativeItem mixer2Item = new NativeItem("Mixer");
        mixer2Item.Activated += (sender, e) => SpawnVehicle("Mixer");
        industrialSubMenu.Add(mixer2Item);

        NativeItem rubbleItem = new NativeItem("Rubble");
        rubbleItem.Activated += (sender, e) => SpawnVehicle("Rubble");
        industrialSubMenu.Add(rubbleItem);

        NativeItem tipTruckItem = new NativeItem("Tipper");
        tipTruckItem.Activated += (sender, e) => SpawnVehicle("Tipper");
        industrialSubMenu.Add(tipTruckItem);

        NativeItem tipTruck2Item = new NativeItem("Tipper");
        tipTruck2Item.Activated += (sender, e) => SpawnVehicle("Tipper");
        industrialSubMenu.Add(tipTruck2Item);

        // Military

        NativeItem apcItem = new NativeItem("APC");
        apcItem.Activated += (sender, e) => SpawnVehicle("APC");
        militarySubMenu.Add(apcItem);

        NativeItem barracksItem = new NativeItem("Barracks");
        barracksItem.Activated += (sender, e) => SpawnVehicle("Barracks");
        militarySubMenu.Add(barracksItem);

        NativeItem barracks2Item = new NativeItem("Barracks Semi");
        barracks2Item.Activated += (sender, e) => SpawnVehicle("Barracks Semi");
        militarySubMenu.Add(barracks2Item);

        NativeItem barracks3Item = new NativeItem("Barracks");
        barracks3Item.Activated += (sender, e) => SpawnVehicle("Barracks");
        militarySubMenu.Add(barracks3Item);

        NativeItem barrageItem = new NativeItem("Barrage");
        barrageItem.Activated += (sender, e) => SpawnVehicle("Barrage");
        militarySubMenu.Add(barrageItem);

        NativeItem chernobogItem = new NativeItem("Chernobog");
        chernobogItem.Activated += (sender, e) => SpawnVehicle("Chernobog");
        militarySubMenu.Add(chernobogItem);

        NativeItem crusaderItem = new NativeItem("Crusader");
        crusaderItem.Activated += (sender, e) => SpawnVehicle("Crusader");
        militarySubMenu.Add(crusaderItem);

        NativeItem halftrackItem = new NativeItem("Half-track");
        halftrackItem.Activated += (sender, e) => SpawnVehicle("Half-track");
        militarySubMenu.Add(halftrackItem);

        NativeItem khanjaliItem = new NativeItem("TM-02 Khanjali");
        khanjaliItem.Activated += (sender, e) => SpawnVehicle("TM-02 Khanjali");
        militarySubMenu.Add(khanjaliItem);

        NativeItem minitankItem = new NativeItem("Invade and Persuade Tank");
        minitankItem.Activated += (sender, e) => SpawnVehicle("Invade and Persuade Tank");
        militarySubMenu.Add(minitankItem);

        NativeItem rhinoItem = new NativeItem("Rhino Tank");
        rhinoItem.Activated += (sender, e) => SpawnVehicle("Rhino Tank");
        militarySubMenu.Add(rhinoItem);

        NativeItem scarabItem = new NativeItem("Apocalypse Scarab");
        scarabItem.Activated += (sender, e) => SpawnVehicle("Apocalypse Scarab");
        militarySubMenu.Add(scarabItem);

        NativeItem scarab2Item = new NativeItem("Future Shock Scarab");
        scarab2Item.Activated += (sender, e) => SpawnVehicle("Future Shock Scarab");
        militarySubMenu.Add(scarab2Item);

        NativeItem scarab3Item = new NativeItem("Nightmare Scarab");
        scarab3Item.Activated += (sender, e) => SpawnVehicle("Nightmare Scarab");
        militarySubMenu.Add(scarab3Item);

        NativeItem thrusterItem = new NativeItem("Thruster");
        thrusterItem.Activated += (sender, e) => SpawnVehicle("Thruster");
        militarySubMenu.Add(thrusterItem);

        NativeItem antiAircraftTrailerItem = new NativeItem("Anti-Aircraft Trailer");
        antiAircraftTrailerItem.Activated += (sender, e) => SpawnVehicle("Anti-Aircraft Trailer");
        militarySubMenu.Add(antiAircraftTrailerItem);

        NativeItem vetirItem = new NativeItem("Vetir");
        vetirItem.Activated += (sender, e) => SpawnVehicle("Vetir");
        militarySubMenu.Add(vetirItem);

        // Motorcycle

        NativeItem akumaItem = new NativeItem("Akuma");
        akumaItem.Activated += (sender, e) => SpawnVehicle("Akuma");
        motorcycleSubMenu.Add(akumaItem);

        NativeItem avarusItem = new NativeItem("Avarus");
        avarusItem.Activated += (sender, e) => SpawnVehicle("Avarus");
        motorcycleSubMenu.Add(avarusItem);

        NativeItem baggerItem = new NativeItem("Bagger");
        baggerItem.Activated += (sender, e) => SpawnVehicle("Bagger");
        motorcycleSubMenu.Add(baggerItem);

        NativeItem bati801Item = new NativeItem("Bati 801");
        bati801Item.Activated += (sender, e) => SpawnVehicle("Bati 801");
        motorcycleSubMenu.Add(bati801Item);

        NativeItem bati801RRItem = new NativeItem("Bati 801RR");
        bati801RRItem.Activated += (sender, e) => SpawnVehicle("Bati 801RR");
        motorcycleSubMenu.Add(bati801RRItem);

        NativeItem bf400Item = new NativeItem("BF400");
        bf400Item.Activated += (sender, e) => SpawnVehicle("BF400");
        motorcycleSubMenu.Add(bf400Item);

        NativeItem carbonRSItem = new NativeItem("Carbon RS");
        carbonRSItem.Activated += (sender, e) => SpawnVehicle("Carbon RS");
        motorcycleSubMenu.Add(carbonRSItem);

        NativeItem chimeraItem = new NativeItem("Chimera");
        chimeraItem.Activated += (sender, e) => SpawnVehicle("Chimera");
        motorcycleSubMenu.Add(chimeraItem);

        NativeItem cliffhangerItem = new NativeItem("Cliffhanger");
        cliffhangerItem.Activated += (sender, e) => SpawnVehicle("Cliffhanger");
        motorcycleSubMenu.Add(cliffhangerItem);

        NativeItem daemon1Item = new NativeItem("Daemon");
        daemon1Item.Activated += (sender, e) => SpawnVehicle("Daemon");
        motorcycleSubMenu.Add(daemon1Item);

        NativeItem daemon2Item = new NativeItem("Daemon");
        daemon2Item.Activated += (sender, e) => SpawnVehicle("Daemon");
        motorcycleSubMenu.Add(daemon2Item);

        NativeItem deathbike1Item = new NativeItem("Apocalypse Deathbike");
        deathbike1Item.Activated += (sender, e) => SpawnVehicle("Apocalypse Deathbike");
        motorcycleSubMenu.Add(deathbike1Item);

        NativeItem deathbike2Item = new NativeItem("Future Shock Deathbike");
        deathbike2Item.Activated += (sender, e) => SpawnVehicle("Future Shock Deathbike");
        motorcycleSubMenu.Add(deathbike2Item);

        NativeItem deathbike3Item = new NativeItem("Nightmare Deathbike");
        deathbike3Item.Activated += (sender, e) => SpawnVehicle("Nightmare Deathbike");
        motorcycleSubMenu.Add(deathbike3Item);

        NativeItem defilerItem = new NativeItem("Defiler");
        defilerItem.Activated += (sender, e) => SpawnVehicle("Defiler");
        motorcycleSubMenu.Add(defilerItem);

        NativeItem diablous1Item = new NativeItem("Diabolus");
        diablous1Item.Activated += (sender, e) => SpawnVehicle("Diabolus");
        motorcycleSubMenu.Add(diablous1Item);

        NativeItem diablous2Item = new NativeItem("Diabolus Custom");
        diablous2Item.Activated += (sender, e) => SpawnVehicle("Diabolus Custom");
        motorcycleSubMenu.Add(diablous2Item);

        NativeItem doubleTItem = new NativeItem("Double-T");
        doubleTItem.Activated += (sender, e) => SpawnVehicle("Double-T");
        motorcycleSubMenu.Add(doubleTItem);

        NativeItem enduroItem = new NativeItem("Enduro");
        enduroItem.Activated += (sender, e) => SpawnVehicle("Enduro");
        motorcycleSubMenu.Add(enduroItem);

        NativeItem esskeyItem = new NativeItem("Esskey");
        esskeyItem.Activated += (sender, e) => SpawnVehicle("Esskey");
        motorcycleSubMenu.Add(esskeyItem);

        NativeItem faggioSportItem = new NativeItem("Faggio Sport");
        faggioSportItem.Activated += (sender, e) => SpawnVehicle("Faggio Sport");
        motorcycleSubMenu.Add(faggioSportItem);

        NativeItem faggioItem = new NativeItem("Faggio");
        faggioItem.Activated += (sender, e) => SpawnVehicle("Faggio");
        motorcycleSubMenu.Add(faggioItem);

        NativeItem faggioModItem = new NativeItem("Faggio Mod");
        faggioModItem.Activated += (sender, e) => SpawnVehicle("Faggio Mod");
        motorcycleSubMenu.Add(faggioModItem);

        NativeItem fcr1000Item = new NativeItem("FCR 1000");
        fcr1000Item.Activated += (sender, e) => SpawnVehicle("FCR 1000");
        motorcycleSubMenu.Add(fcr1000Item);

        NativeItem fcr1000CustomItem = new NativeItem("FCR 1000 Custom");
        fcr1000CustomItem.Activated += (sender, e) => SpawnVehicle("FCR 1000 Custom");
        motorcycleSubMenu.Add(fcr1000CustomItem);

        NativeItem gargoyleItem = new NativeItem("Gargoyle");
        gargoyleItem.Activated += (sender, e) => SpawnVehicle("Gargoyle");
        motorcycleSubMenu.Add(gargoyleItem);

        NativeItem hakuchouItem = new NativeItem("Hakuchou");
        hakuchouItem.Activated += (sender, e) => SpawnVehicle("Hakuchou");
        motorcycleSubMenu.Add(hakuchouItem);

        NativeItem hakuchouDragItem = new NativeItem("Hakuchou Drag");
        hakuchouDragItem.Activated += (sender, e) => SpawnVehicle("Hakuchou Drag");
        motorcycleSubMenu.Add(hakuchouDragItem);

        NativeItem hexerItem = new NativeItem("Hexer");
        hexerItem.Activated += (sender, e) => SpawnVehicle("Hexer");
        motorcycleSubMenu.Add(hexerItem);

        NativeItem innovationItem = new NativeItem("Innovation");
        innovationItem.Activated += (sender, e) => SpawnVehicle("Innovation");
        motorcycleSubMenu.Add(innovationItem);

        NativeItem lectroItem = new NativeItem("Lectro");
        lectroItem.Activated += (sender, e) => SpawnVehicle("Lectro");
        motorcycleSubMenu.Add(lectroItem);

        NativeItem manchez1Item = new NativeItem("Manchez");
        manchez1Item.Activated += (sender, e) => SpawnVehicle("Manchez");
        motorcycleSubMenu.Add(manchez1Item);

        NativeItem manchez2Item = new NativeItem("Manchez Scout");
        manchez2Item.Activated += (sender, e) => SpawnVehicle("Manchez Scout");
        motorcycleSubMenu.Add(manchez2Item);

        NativeItem manchez3Item = new NativeItem("Manchez Scout C");
        manchez3Item.Activated += (sender, e) => SpawnVehicle("Manchez Scout C");
        motorcycleSubMenu.Add(manchez3Item);

        NativeItem nemesisItem = new NativeItem("Nemesis");
        nemesisItem.Activated += (sender, e) => SpawnVehicle("Nemesis");
        motorcycleSubMenu.Add(nemesisItem);

        NativeItem nightbladeItem = new NativeItem("Nightblade");
        nightbladeItem.Activated += (sender, e) => SpawnVehicle("Nightblade");
        motorcycleSubMenu.Add(nightbladeItem);

        NativeItem oppressorItem = new NativeItem("Oppressor");
        oppressorItem.Activated += (sender, e) => SpawnVehicle("Oppressor");
        motorcycleSubMenu.Add(oppressorItem);

        NativeItem oppressorMk2Item = new NativeItem("Oppressor Mk II");
        oppressorMk2Item.Activated += (sender, e) => SpawnVehicle("Oppressor Mk II");
        motorcycleSubMenu.Add(oppressorMk2Item);

        NativeItem pcj600Item = new NativeItem("PCJ 600");
        pcj600Item.Activated += (sender, e) => SpawnVehicle("PCJ 600");
        motorcycleSubMenu.Add(pcj600Item);

        NativeItem powersurgeItem = new NativeItem("Powersurge");
        powersurgeItem.Activated += (sender, e) => SpawnVehicle("Powersurge");
        motorcycleSubMenu.Add(powersurgeItem);

        NativeItem ratBikeItem = new NativeItem("Rat Bike");
        ratBikeItem.Activated += (sender, e) => SpawnVehicle("Rat Bike");
        motorcycleSubMenu.Add(ratBikeItem);

        NativeItem reeverItem = new NativeItem("Reever");
        reeverItem.Activated += (sender, e) => SpawnVehicle("Reever");
        motorcycleSubMenu.Add(reeverItem);

        NativeItem rampantRocketItem = new NativeItem("Rampant Rocket");
        rampantRocketItem.Activated += (sender, e) => SpawnVehicle("Rampant Rocket");
        motorcycleSubMenu.Add(rampantRocketItem);

        NativeItem ruffianItem = new NativeItem("Ruffian");
        ruffianItem.Activated += (sender, e) => SpawnVehicle("Ruffian");
        motorcycleSubMenu.Add(ruffianItem);

        NativeItem sanchezLiveryItem = new NativeItem("Sanchez (livery)");
        sanchezLiveryItem.Activated += (sender, e) => SpawnVehicle("Sanchez (livery)");
        motorcycleSubMenu.Add(sanchezLiveryItem);

        NativeItem sanchezItem = new NativeItem("Sanchez");
        sanchezItem.Activated += (sender, e) => SpawnVehicle("Sanchez");
        motorcycleSubMenu.Add(sanchezItem);

        NativeItem sanctusItem = new NativeItem("Sanctus");
        sanctusItem.Activated += (sender, e) => SpawnVehicle("Sanctus");
        motorcycleSubMenu.Add(sanctusItem);

        NativeItem shinobiItem = new NativeItem("Shinobi");
        shinobiItem.Activated += (sender, e) => SpawnVehicle("Shinobi");
        motorcycleSubMenu.Add(shinobiItem);

        NativeItem shotaroItem = new NativeItem("Shotaro");
        shotaroItem.Activated += (sender, e) => SpawnVehicle("Shotaro");
        motorcycleSubMenu.Add(shotaroItem);

        NativeItem sovereignItem = new NativeItem("Sovereign");
        sovereignItem.Activated += (sender, e) => SpawnVehicle("Sovereign");
        motorcycleSubMenu.Add(sovereignItem);

        NativeItem stryderItem = new NativeItem("Stryder");
        stryderItem.Activated += (sender, e) => SpawnVehicle("Stryder");
        motorcycleSubMenu.Add(stryderItem);

        NativeItem thrustItem = new NativeItem("Thrust");
        thrustItem.Activated += (sender, e) => SpawnVehicle("Thrust");
        motorcycleSubMenu.Add(thrustItem);

        NativeItem vaderItem = new NativeItem("Vader");
        vaderItem.Activated += (sender, e) => SpawnVehicle("Vader");
        motorcycleSubMenu.Add(vaderItem);

        NativeItem vindicatorItem = new NativeItem("Vindicator");
        vindicatorItem.Activated += (sender, e) => SpawnVehicle("Vindicator");
        motorcycleSubMenu.Add(vindicatorItem);

        NativeItem vortexItem = new NativeItem("Vortex");
        vortexItem.Activated += (sender, e) => SpawnVehicle("Vortex");
        motorcycleSubMenu.Add(vortexItem);

        NativeItem wolfsbaneItem = new NativeItem("Wolfsbane");
        wolfsbaneItem.Activated += (sender, e) => SpawnVehicle("Wolfsbane");
        motorcycleSubMenu.Add(wolfsbaneItem);

        NativeItem zombieBobberItem = new NativeItem("Zombie Bobber");
        zombieBobberItem.Activated += (sender, e) => SpawnVehicle("Zombie Bobber");
        motorcycleSubMenu.Add(zombieBobberItem);

        NativeItem zombieChopperItem = new NativeItem("Zombie Chopper");
        zombieChopperItem.Activated += (sender, e) => SpawnVehicle("Zombie Chopper");
        motorcycleSubMenu.Add(zombieChopperItem);

        // Muscle

        NativeItem bladeItem = new NativeItem("Blade");
        bladeItem.Activated += (sender, e) => SpawnVehicle("Blade");
        muscleSubMenu.Add(bladeItem);

        NativeItem broadwayItem = new NativeItem("Broadway");
        broadwayItem.Activated += (sender, e) => SpawnVehicle("Broadway");
        muscleSubMenu.Add(broadwayItem);

        NativeItem buccaneerItem = new NativeItem("Buccaneer");
        buccaneerItem.Activated += (sender, e) => SpawnVehicle("Buccaneer");
        muscleSubMenu.Add(buccaneerItem);

        NativeItem buccaneerCustomItem = new NativeItem("Buccaneer Custom");
        buccaneerCustomItem.Activated += (sender, e) => SpawnVehicle("Buccaneer Custom");
        muscleSubMenu.Add(buccaneerCustomItem);

        NativeItem buffaloSTXItem = new NativeItem("Buffalo STX");
        buffaloSTXItem.Activated += (sender, e) => SpawnVehicle("Buffalo STX");
        muscleSubMenu.Add(buffaloSTXItem);

        NativeItem chinoItem = new NativeItem("Chino");
        chinoItem.Activated += (sender, e) => SpawnVehicle("Chino");
        muscleSubMenu.Add(chinoItem);

        NativeItem chinoCustomItem = new NativeItem("Chino Custom");
        chinoCustomItem.Activated += (sender, e) => SpawnVehicle("Chino Custom");
        muscleSubMenu.Add(chinoCustomItem);

        NativeItem cliqueItem = new NativeItem("Clique");
        cliqueItem.Activated += (sender, e) => SpawnVehicle("Clique");
        muscleSubMenu.Add(cliqueItem);

        NativeItem coquetteBlackFinItem = new NativeItem("Coquette BlackFin");
        coquetteBlackFinItem.Activated += (sender, e) => SpawnVehicle("Coquette BlackFin");
        muscleSubMenu.Add(coquetteBlackFinItem);

        NativeItem deviantItem = new NativeItem("Deviant");
        deviantItem.Activated += (sender, e) => SpawnVehicle("Deviant");
        muscleSubMenu.Add(deviantItem);

        NativeItem dominatorItem = new NativeItem("Dominator");
        dominatorItem.Activated += (sender, e) => SpawnVehicle("Dominator");
        muscleSubMenu.Add(dominatorItem);

        NativeItem pisswasserDominatorItem = new NativeItem("Pisswasser Dominator");
        pisswasserDominatorItem.Activated += (sender, e) => SpawnVehicle("Pisswasser Dominator");
        muscleSubMenu.Add(pisswasserDominatorItem);

        NativeItem dominatorGTXItem = new NativeItem("Dominator GTX");
        dominatorGTXItem.Activated += (sender, e) => SpawnVehicle("Dominator GTX");
        muscleSubMenu.Add(dominatorGTXItem);

        NativeItem apocalypseDominatorItem = new NativeItem("Apocalypse Dominator");
        apocalypseDominatorItem.Activated += (sender, e) => SpawnVehicle("Apocalypse Dominator");
        muscleSubMenu.Add(apocalypseDominatorItem);

        NativeItem futureShockDominatorItem = new NativeItem("Future Shock Dominator");
        futureShockDominatorItem.Activated += (sender, e) => SpawnVehicle("Future Shock Dominator");
        muscleSubMenu.Add(futureShockDominatorItem);

        NativeItem nightmareDominatorItem = new NativeItem("Nightmare Dominator");
        nightmareDominatorItem.Activated += (sender, e) => SpawnVehicle("Nightmare Dominator");
        muscleSubMenu.Add(nightmareDominatorItem);

        NativeItem dominatorASPItem = new NativeItem("Dominator ASP");
        dominatorASPItem.Activated += (sender, e) => SpawnVehicle("Dominator ASP");
        muscleSubMenu.Add(dominatorASPItem);

        NativeItem dominatorGTTItem = new NativeItem("Dominator GTT");
        dominatorGTTItem.Activated += (sender, e) => SpawnVehicle("Dominator GTT");
        muscleSubMenu.Add(dominatorGTTItem);

        NativeItem dukesItem = new NativeItem("Dukes");
        dukesItem.Activated += (sender, e) => SpawnVehicle("Dukes");
        muscleSubMenu.Add(dukesItem);

        NativeItem dukeODeathItem = new NativeItem("Duke O'Death");
        dukeODeathItem.Activated += (sender, e) => SpawnVehicle("Duke O'Death");
        muscleSubMenu.Add(dukeODeathItem);

        NativeItem beaterDukesItem = new NativeItem("Beater Dukes");
        beaterDukesItem.Activated += (sender, e) => SpawnVehicle("Beater Dukes");
        muscleSubMenu.Add(beaterDukesItem);

        NativeItem ellieItem = new NativeItem("Ellie");
        ellieItem.Activated += (sender, e) => SpawnVehicle("Ellie");
        muscleSubMenu.Add(ellieItem);

        NativeItem eudoraItem = new NativeItem("Eudora");
        eudoraItem.Activated += (sender, e) => SpawnVehicle("Eudora");
        muscleSubMenu.Add(eudoraItem);

        NativeItem factionItem = new NativeItem("Faction");
        factionItem.Activated += (sender, e) => SpawnVehicle("Faction");
        muscleSubMenu.Add(factionItem);

        NativeItem factionCustomItem = new NativeItem("Faction Custom");
        factionCustomItem.Activated += (sender, e) => SpawnVehicle("Faction Custom");
        muscleSubMenu.Add(factionCustomItem);

        NativeItem factionCustomDonkItem = new NativeItem("Faction Custom Donk");
        factionCustomDonkItem.Activated += (sender, e) => SpawnVehicle("Faction Custom Donk");
        muscleSubMenu.Add(factionCustomDonkItem);

        NativeItem gauntletItem = new NativeItem("Gauntlet");
        gauntletItem.Activated += (sender, e) => SpawnVehicle("Gauntlet");
        muscleSubMenu.Add(gauntletItem);

        NativeItem redwoodGauntletItem = new NativeItem("Redwood Gauntlet");
        redwoodGauntletItem.Activated += (sender, e) => SpawnVehicle("Redwood Gauntlet");
        muscleSubMenu.Add(redwoodGauntletItem);

        NativeItem gauntletClassicItem = new NativeItem("Gauntlet Classic");
        gauntletClassicItem.Activated += (sender, e) => SpawnVehicle("Gauntlet Classic");
        muscleSubMenu.Add(gauntletClassicItem);

        NativeItem gauntletHellfireItem = new NativeItem("Gauntlet Hellfire");
        gauntletHellfireItem.Activated += (sender, e) => SpawnVehicle("Gauntlet Hellfire");
        muscleSubMenu.Add(gauntletHellfireItem);

        NativeItem gauntletClassicCustomItem = new NativeItem("Gauntlet Classic Custom");
        gauntletClassicCustomItem.Activated += (sender, e) => SpawnVehicle("Gauntlet Classic Custom");
        muscleSubMenu.Add(gauntletClassicCustomItem);

        NativeItem greenwoodItem = new NativeItem("Greenwood");
        greenwoodItem.Activated += (sender, e) => SpawnVehicle("Greenwood");
        muscleSubMenu.Add(greenwoodItem);

        NativeItem hermesItem = new NativeItem("Hermes");
        hermesItem.Activated += (sender, e) => SpawnVehicle("Hermes");
        muscleSubMenu.Add(hermesItem);

        NativeItem hotknifeItem = new NativeItem("Hotknife");
        hotknifeItem.Activated += (sender, e) => SpawnVehicle("Hotknife");
        muscleSubMenu.Add(hotknifeItem);

        NativeItem hustlerItem = new NativeItem("Hustler");
        hustlerItem.Activated += (sender, e) => SpawnVehicle("Hustler");
        muscleSubMenu.Add(hustlerItem);

        NativeItem impalerItem = new NativeItem("Impaler");
        impalerItem.Activated += (sender, e) => SpawnVehicle("Impaler");
        muscleSubMenu.Add(impalerItem);

        NativeItem apocalypseImpalerItem = new NativeItem("Apocalypse Impaler");
        apocalypseImpalerItem.Activated += (sender, e) => SpawnVehicle("Apocalypse Impaler");
        muscleSubMenu.Add(apocalypseImpalerItem);

        NativeItem futureShockImpalerItem = new NativeItem("Future Shock Impaler");
        futureShockImpalerItem.Activated += (sender, e) => SpawnVehicle("Future Shock Impaler");
        muscleSubMenu.Add(futureShockImpalerItem);

        NativeItem nightmareImpalerItem = new NativeItem("Nightmare Impaler");
        nightmareImpalerItem.Activated += (sender, e) => SpawnVehicle("Nightmare Impaler");
        muscleSubMenu.Add(nightmareImpalerItem);

        NativeItem apocalypseImperatorItem = new NativeItem("Apocalypse Imperator");
        apocalypseImperatorItem.Activated += (sender, e) => SpawnVehicle("Apocalypse Imperator");
        muscleSubMenu.Add(apocalypseImperatorItem);

        NativeItem futureShockImperatorItem = new NativeItem("Future Shock Imperator");
        futureShockImperatorItem.Activated += (sender, e) => SpawnVehicle("Future Shock Imperator");
        muscleSubMenu.Add(futureShockImperatorItem);

        NativeItem nightmareImperatorItem = new NativeItem("Nightmare Imperator");
        nightmareImperatorItem.Activated += (sender, e) => SpawnVehicle("Nightmare Imperator");
        muscleSubMenu.Add(nightmareImperatorItem);

        NativeItem lurcherItem = new NativeItem("Lurcher");
        lurcherItem.Activated += (sender, e) => SpawnVehicle("Lurcher");
        muscleSubMenu.Add(lurcherItem);

        NativeItem mananaCustomItem = new NativeItem("Manana Custom");
        mananaCustomItem.Activated += (sender, e) => SpawnVehicle("Manana Custom");
        muscleSubMenu.Add(mananaCustomItem);

        NativeItem moonbeamItem = new NativeItem("Moonbeam");
        moonbeamItem.Activated += (sender, e) => SpawnVehicle("Moonbeam");
        muscleSubMenu.Add(moonbeamItem);

        NativeItem moonbeamCustomItem = new NativeItem("Moonbeam Custom");
        moonbeamCustomItem.Activated += (sender, e) => SpawnVehicle("Moonbeam Custom");
        muscleSubMenu.Add(moonbeamCustomItem);

        NativeItem nightshadeItem = new NativeItem("Nightshade");
        nightshadeItem.Activated += (sender, e) => SpawnVehicle("Nightshade");
        muscleSubMenu.Add(nightshadeItem);

        NativeItem peyoteGasserItem = new NativeItem("Peyote Gasser");
        peyoteGasserItem.Activated += (sender, e) => SpawnVehicle("Peyote Gasser");
        muscleSubMenu.Add(peyoteGasserItem);

        NativeItem phoenixItem = new NativeItem("Phoenix");
        phoenixItem.Activated += (sender, e) => SpawnVehicle("Phoenix");
        muscleSubMenu.Add(phoenixItem);

        NativeItem picadorItem = new NativeItem("Picador");
        picadorItem.Activated += (sender, e) => SpawnVehicle("Picador");
        muscleSubMenu.Add(picadorItem);

        NativeItem ratLoaderItem = new NativeItem("Rat-Loader");
        ratLoaderItem.Activated += (sender, e) => SpawnVehicle("Rat-Loader");
        muscleSubMenu.Add(ratLoaderItem);

        NativeItem ratTruckItem = new NativeItem("Rat-Truck");
        ratTruckItem.Activated += (sender, e) => SpawnVehicle("Rat-Truck");
        muscleSubMenu.Add(ratTruckItem);

        NativeItem ruinerItem = new NativeItem("Ruiner");
        ruinerItem.Activated += (sender, e) => SpawnVehicle("Ruiner");
        muscleSubMenu.Add(ruinerItem);

        NativeItem ruiner2000Item = new NativeItem("Ruiner 2000");
        ruiner2000Item.Activated += (sender, e) => SpawnVehicle("Ruiner 2000");
        muscleSubMenu.Add(ruiner2000Item);

        NativeItem ruinerItem2 = new NativeItem("Ruiner");
        ruinerItem2.Activated += (sender, e) => SpawnVehicle("Ruiner");
        muscleSubMenu.Add(ruinerItem2);

        NativeItem ruinerZZ8Item = new NativeItem("Ruiner ZZ-8");
        ruinerZZ8Item.Activated += (sender, e) => SpawnVehicle("Ruiner ZZ-8");
        muscleSubMenu.Add(ruinerZZ8Item);

        NativeItem sabreTurboItem = new NativeItem("Sabre Turbo");
        sabreTurboItem.Activated += (sender, e) => SpawnVehicle("Sabre Turbo");
        muscleSubMenu.Add(sabreTurboItem);

        NativeItem sabreTurboCustomItem = new NativeItem("Sabre Turbo Custom");
        sabreTurboCustomItem.Activated += (sender, e) => SpawnVehicle("Sabre Turbo Custom");
        muscleSubMenu.Add(sabreTurboCustomItem);

        NativeItem slamvanItem = new NativeItem("Slamvan");
        slamvanItem.Activated += (sender, e) => SpawnVehicle("Slamvan");
        muscleSubMenu.Add(slamvanItem);

        NativeItem lostSlamvanItem = new NativeItem("Lost Slamvan");
        lostSlamvanItem.Activated += (sender, e) => SpawnVehicle("Lost Slamvan");
        muscleSubMenu.Add(lostSlamvanItem);

        NativeItem slamvanCustomItem = new NativeItem("Slamvan Custom");
        slamvanCustomItem.Activated += (sender, e) => SpawnVehicle("Slamvan Custom");
        muscleSubMenu.Add(slamvanCustomItem);

        NativeItem apocalypseSlamvanItem = new NativeItem("Apocalypse Slamvan");
        apocalypseSlamvanItem.Activated += (sender, e) => SpawnVehicle("Apocalypse Slamvan");
        muscleSubMenu.Add(apocalypseSlamvanItem);

        NativeItem futureShockSlamvanItem = new NativeItem("Future Shock Slamvan");
        futureShockSlamvanItem.Activated += (sender, e) => SpawnVehicle("Future Shock Slamvan");
        muscleSubMenu.Add(futureShockSlamvanItem);

        NativeItem nightmareSlamvanItem = new NativeItem("Nightmare Slamvan");
        nightmareSlamvanItem.Activated += (sender, e) => SpawnVehicle("Nightmare Slamvan");
        muscleSubMenu.Add(nightmareSlamvanItem);

        NativeItem stallionItem = new NativeItem("Stallion");
        stallionItem.Activated += (sender, e) => SpawnVehicle("Stallion");
        muscleSubMenu.Add(stallionItem);

        NativeItem burgerShotStallionItem = new NativeItem("Burger Shot Stallion");
        burgerShotStallionItem.Activated += (sender, e) => SpawnVehicle("Burger Shot Stallion");
        muscleSubMenu.Add(burgerShotStallionItem);

        NativeItem tahomaCoupeItem = new NativeItem("Tahoma Coupe");
        tahomaCoupeItem.Activated += (sender, e) => SpawnVehicle("Tahoma Coupe");
        muscleSubMenu.Add(tahomaCoupeItem);

        NativeItem tampaItem = new NativeItem("Tampa");
        tampaItem.Activated += (sender, e) => SpawnVehicle("Tampa");
        muscleSubMenu.Add(tampaItem);

        NativeItem weaponizedTampaItem = new NativeItem("Weaponized Tampa");
        weaponizedTampaItem.Activated += (sender, e) => SpawnVehicle("Weaponized Tampa");
        muscleSubMenu.Add(weaponizedTampaItem);

        NativeItem tulipItem = new NativeItem("Tulip");
        tulipItem.Activated += (sender, e) => SpawnVehicle("Tulip");
        muscleSubMenu.Add(tulipItem);

        NativeItem tulipM100Item = new NativeItem("Tulip M-100");
        tulipM100Item.Activated += (sender, e) => SpawnVehicle("Tulip M-100");
        muscleSubMenu.Add(tulipM100Item);

        NativeItem vamosItem = new NativeItem("Vamos");
        vamosItem.Activated += (sender, e) => SpawnVehicle("Vamos");
        muscleSubMenu.Add(vamosItem);

        NativeItem vigeroItem = new NativeItem("Vigero");
        vigeroItem.Activated += (sender, e) => SpawnVehicle("Vigero");
        muscleSubMenu.Add(vigeroItem);

        NativeItem vigeroZXItem = new NativeItem("Vigero ZX");
        vigeroZXItem.Activated += (sender, e) => SpawnVehicle("Vigero ZX");
        muscleSubMenu.Add(vigeroZXItem);

        NativeItem virgoItem = new NativeItem("Virgo");
        virgoItem.Activated += (sender, e) => SpawnVehicle("Virgo");
        muscleSubMenu.Add(virgoItem);

        NativeItem virgoClassicCustomItem = new NativeItem("Virgo Classic Custom");
        virgoClassicCustomItem.Activated += (sender, e) => SpawnVehicle("Virgo Classic Custom");
        muscleSubMenu.Add(virgoClassicCustomItem);

        NativeItem virgoClassicItem = new NativeItem("Virgo Classic");
        virgoClassicItem.Activated += (sender, e) => SpawnVehicle("Virgo Classic");
        muscleSubMenu.Add(virgoClassicItem);

        NativeItem voodooCustomItem = new NativeItem("Voodoo Custom");
        voodooCustomItem.Activated += (sender, e) => SpawnVehicle("Voodoo Custom");
        muscleSubMenu.Add(voodooCustomItem);

        NativeItem voodooItem = new NativeItem("Voodoo");
        voodooItem.Activated += (sender, e) => SpawnVehicle("Voodoo");
        muscleSubMenu.Add(voodooItem);

        NativeItem weevilCustomItem = new NativeItem("Weevil Custom");
        weevilCustomItem.Activated += (sender, e) => SpawnVehicle("Weevil Custom");
        muscleSubMenu.Add(weevilCustomItem);

        NativeItem yosemiteItem = new NativeItem("Yosemite");
        yosemiteItem.Activated += (sender, e) => SpawnVehicle("Yosemite");
        muscleSubMenu.Add(yosemiteItem);

        NativeItem driftYosemiteItem = new NativeItem("Drift Yosemite");
        driftYosemiteItem.Activated += (sender, e) => SpawnVehicle("Drift Yosemite");
        muscleSubMenu.Add(driftYosemiteItem);


        // Off-Road

        NativeItem bfInjectionItem = new NativeItem("BfInjection");
        bfInjectionItem.Activated += (sender, e) => SpawnVehicle("BfInjection");
        offRoadSubMenu.Add(bfInjectionItem);

        NativeItem biftaItem = new NativeItem("bifta");
        biftaItem.Activated += (sender, e) => SpawnVehicle("bifta");
        offRoadSubMenu.Add(biftaItem);

        NativeItem blazerItem = new NativeItem("blazer");
        blazerItem.Activated += (sender, e) => SpawnVehicle("blazer");
        offRoadSubMenu.Add(blazerItem);

        NativeItem blazer2Item = new NativeItem("blazer2");
        blazer2Item.Activated += (sender, e) => SpawnVehicle("blazer2");
        offRoadSubMenu.Add(blazer2Item);

        NativeItem blazer3Item = new NativeItem("blazer3");
        blazer3Item.Activated += (sender, e) => SpawnVehicle("blazer3");
        offRoadSubMenu.Add(blazer3Item);

        NativeItem blazer4Item = new NativeItem("blazer4");
        blazer4Item.Activated += (sender, e) => SpawnVehicle("blazer4");
        offRoadSubMenu.Add(blazer4Item);

        NativeItem blazer5Item = new NativeItem("blazer5");
        blazer5Item.Activated += (sender, e) => SpawnVehicle("blazer5");
        offRoadSubMenu.Add(blazer5Item);

        NativeItem bodhi2Item = new NativeItem("Bodhi2");
        bodhi2Item.Activated += (sender, e) => SpawnVehicle("Bodhi2");
        offRoadSubMenu.Add(bodhi2Item);

        NativeItem boorItem = new NativeItem("boor");
        boorItem.Activated += (sender, e) => SpawnVehicle("boor");
        offRoadSubMenu.Add(boorItem);

        NativeItem brawlerItem = new NativeItem("brawler");
        brawlerItem.Activated += (sender, e) => SpawnVehicle("brawler");
        offRoadSubMenu.Add(brawlerItem);

        NativeItem bruiserItem = new NativeItem("bruiser");
        bruiserItem.Activated += (sender, e) => SpawnVehicle("bruiser");
        offRoadSubMenu.Add(bruiserItem);

        NativeItem bruiser2Item = new NativeItem("bruiser2");
        bruiser2Item.Activated += (sender, e) => SpawnVehicle("bruiser2");
        offRoadSubMenu.Add(bruiser2Item);

        NativeItem bruiser3Item = new NativeItem("bruiser3");
        bruiser3Item.Activated += (sender, e) => SpawnVehicle("bruiser3");
        offRoadSubMenu.Add(bruiser3Item);

        NativeItem brutusItem = new NativeItem("brutus");
        brutusItem.Activated += (sender, e) => SpawnVehicle("brutus");
        offRoadSubMenu.Add(brutusItem);

        NativeItem brutus2Item = new NativeItem("brutus2");
        brutus2Item.Activated += (sender, e) => SpawnVehicle("brutus2");
        offRoadSubMenu.Add(brutus2Item);

        NativeItem brutus3Item = new NativeItem("brutus3");
        brutus3Item.Activated += (sender, e) => SpawnVehicle("brutus3");
        offRoadSubMenu.Add(brutus3Item);

        NativeItem caracaraItem = new NativeItem("caracara");
        caracaraItem.Activated += (sender, e) => SpawnVehicle("caracara");
        offRoadSubMenu.Add(caracaraItem);

        NativeItem caracara2Item = new NativeItem("caracara2");
        caracara2Item.Activated += (sender, e) => SpawnVehicle("caracara2");
        offRoadSubMenu.Add(caracara2Item);

        NativeItem dloaderItem = new NativeItem("dloader");
        dloaderItem.Activated += (sender, e) => SpawnVehicle("dloader");
        offRoadSubMenu.Add(dloaderItem);

        NativeItem draugurItem = new NativeItem("draugur");
        draugurItem.Activated += (sender, e) => SpawnVehicle("draugur");
        offRoadSubMenu.Add(draugurItem);

        NativeItem dubsta3Item = new NativeItem("dubsta3");
        dubsta3Item.Activated += (sender, e) => SpawnVehicle("dubsta3");
        offRoadSubMenu.Add(dubsta3Item);

        NativeItem duneItem = new NativeItem("dune");
        duneItem.Activated += (sender, e) => SpawnVehicle("dune");
        offRoadSubMenu.Add(duneItem);

        NativeItem dune2Item = new NativeItem("dune2");
        dune2Item.Activated += (sender, e) => SpawnVehicle("dune2");
        offRoadSubMenu.Add(dune2Item);

        NativeItem dune3Item = new NativeItem("dune3");
        dune3Item.Activated += (sender, e) => SpawnVehicle("dune3");
        offRoadSubMenu.Add(dune3Item);

        NativeItem dune4Item = new NativeItem("dune4");
        dune4Item.Activated += (sender, e) => SpawnVehicle("dune4");
        offRoadSubMenu.Add(dune4Item);

        NativeItem dune5Item = new NativeItem("dune5");
        dune5Item.Activated += (sender, e) => SpawnVehicle("dune5");
        offRoadSubMenu.Add(dune5Item);

        NativeItem everonItem = new NativeItem("everon");
        everonItem.Activated += (sender, e) => SpawnVehicle("everon");
        offRoadSubMenu.Add(everonItem);

        NativeItem freecrawlerItem = new NativeItem("freecrawler");
        freecrawlerItem.Activated += (sender, e) => SpawnVehicle("freecrawler");
        offRoadSubMenu.Add(freecrawlerItem);

        NativeItem hellionItem = new NativeItem("hellion");
        hellionItem.Activated += (sender, e) => SpawnVehicle("hellion");
        offRoadSubMenu.Add(hellionItem);

        NativeItem insurgentItem = new NativeItem("insurgent");
        insurgentItem.Activated += (sender, e) => SpawnVehicle("insurgent");
        offRoadSubMenu.Add(insurgentItem);

        NativeItem insurgent2Item = new NativeItem("insurgent2");
        insurgent2Item.Activated += (sender, e) => SpawnVehicle("insurgent2");
        offRoadSubMenu.Add(insurgent2Item);

        NativeItem insurgent3Item = new NativeItem("insurgent3");
        insurgent3Item.Activated += (sender, e) => SpawnVehicle("insurgent3");
        offRoadSubMenu.Add(insurgent3Item);

        NativeItem kalahariItem = new NativeItem("kalahari");
        kalahariItem.Activated += (sender, e) => SpawnVehicle("kalahari");
        offRoadSubMenu.Add(kalahariItem);

        NativeItem kamachoItem = new NativeItem("kamacho");
        kamachoItem.Activated += (sender, e) => SpawnVehicle("kamacho");
        offRoadSubMenu.Add(kamachoItem);

        NativeItem marshallItem = new NativeItem("marshall");
        marshallItem.Activated += (sender, e) => SpawnVehicle("marshall");
        offRoadSubMenu.Add(marshallItem);

        NativeItem menacerItem = new NativeItem("menacer");
        menacerItem.Activated += (sender, e) => SpawnVehicle("menacer");
        offRoadSubMenu.Add(menacerItem);

        NativeItem mesa3Item = new NativeItem("MESA3");
        mesa3Item.Activated += (sender, e) => SpawnVehicle("MESA3");
        offRoadSubMenu.Add(mesa3Item);

        NativeItem monsterItem = new NativeItem("monster");
        monsterItem.Activated += (sender, e) => SpawnVehicle("monster");
        offRoadSubMenu.Add(monsterItem);

        NativeItem monster3Item = new NativeItem("monster3");
        monster3Item.Activated += (sender, e) => SpawnVehicle("monster3");
        offRoadSubMenu.Add(monster3Item);

        NativeItem monster4Item = new NativeItem("monster4");
        monster4Item.Activated += (sender, e) => SpawnVehicle("monster4");
        offRoadSubMenu.Add(monster4Item);

        NativeItem monster5Item = new NativeItem("monster5");
        monster5Item.Activated += (sender, e) => SpawnVehicle("monster5");
        offRoadSubMenu.Add(monster5Item);

        NativeItem nightsharkItem = new NativeItem("nightshark");
        nightsharkItem.Activated += (sender, e) => SpawnVehicle("nightshark");
        offRoadSubMenu.Add(nightsharkItem);

        NativeItem outlawItem = new NativeItem("outlaw");
        outlawItem.Activated += (sender, e) => SpawnVehicle("outlaw");
        offRoadSubMenu.Add(outlawItem);

        NativeItem patriot3Item = new NativeItem("patriot3");
        patriot3Item.Activated += (sender, e) => SpawnVehicle("patriot3");
        offRoadSubMenu.Add(patriot3Item);

        NativeItem rancherXLItem = new NativeItem("RancherXL");
        rancherXLItem.Activated += (sender, e) => SpawnVehicle("RancherXL");
        offRoadSubMenu.Add(rancherXLItem);

        NativeItem rancherXL2Item = new NativeItem("rancherxl2");
        rancherXL2Item.Activated += (sender, e) => SpawnVehicle("rancherxl2");
        offRoadSubMenu.Add(rancherXL2Item);

        NativeItem rcbanditoItem = new NativeItem("rcbandito");
        rcbanditoItem.Activated += (sender, e) => SpawnVehicle("rcbandito");
        offRoadSubMenu.Add(rcbanditoItem);

        NativeItem rebelItem = new NativeItem("Rebel");
        rebelItem.Activated += (sender, e) => SpawnVehicle("Rebel");
        offRoadSubMenu.Add(rebelItem);

        NativeItem rebel2Item = new NativeItem("rebel2");
        rebel2Item.Activated += (sender, e) => SpawnVehicle("rebel2");
        offRoadSubMenu.Add(rebel2Item);

        NativeItem riataItem = new NativeItem("riata");
        riataItem.Activated += (sender, e) => SpawnVehicle("riata");
        offRoadSubMenu.Add(riataItem);

        NativeItem sandkingItem = new NativeItem("sandking");
        sandkingItem.Activated += (sender, e) => SpawnVehicle("sandking");
        offRoadSubMenu.Add(sandkingItem);

        NativeItem sandking2Item = new NativeItem("sandking2");
        sandking2Item.Activated += (sender, e) => SpawnVehicle("sandking2");
        offRoadSubMenu.Add(sandking2Item);

        NativeItem technicalItem = new NativeItem("technical");
        technicalItem.Activated += (sender, e) => SpawnVehicle("technical");
        offRoadSubMenu.Add(technicalItem);

        NativeItem technical2Item = new NativeItem("technical2");
        technical2Item.Activated += (sender, e) => SpawnVehicle("technical2");
        offRoadSubMenu.Add(technical2Item);

        NativeItem technical3Item = new NativeItem("technical3");
        technical3Item.Activated += (sender, e) => SpawnVehicle("technical3");
        offRoadSubMenu.Add(technical3Item);

        NativeItem trophyTruckItem = new NativeItem("trophytruck");
        trophyTruckItem.Activated += (sender, e) => SpawnVehicle("trophytruck");
        offRoadSubMenu.Add(trophyTruckItem);

        NativeItem trophyTruck2Item = new NativeItem("trophytruck2");
        trophyTruck2Item.Activated += (sender, e) => SpawnVehicle("trophytruck2");
        offRoadSubMenu.Add(trophyTruck2Item);

        NativeItem vagrantItem = new NativeItem("vagrant");
        vagrantItem.Activated += (sender, e) => SpawnVehicle("vagrant");
        offRoadSubMenu.Add(vagrantItem);

        NativeItem verusItem = new NativeItem("verus");
        verusItem.Activated += (sender, e) => SpawnVehicle("verus");
        offRoadSubMenu.Add(verusItem);

        NativeItem winkyItem = new NativeItem("winky");
        winkyItem.Activated += (sender, e) => SpawnVehicle("winky");
        offRoadSubMenu.Add(winkyItem);

        NativeItem yosemite3Item = new NativeItem("yosemite3");
        yosemite3Item.Activated += (sender, e) => SpawnVehicle("yosemite3");
        offRoadSubMenu.Add(yosemite3Item);

        NativeItem zhabaItem = new NativeItem("zhaba");
        zhabaItem.Activated += (sender, e) => SpawnVehicle("zhaba");
        offRoadSubMenu.Add(zhabaItem);

        // Open Wheel

        NativeItem formulaItem = new NativeItem("Formula");
        formulaItem.Activated += (sender, e) => SpawnVehicle("Formula");
        openWheelSubMenu.Add(formulaItem);

        NativeItem formula2Item = new NativeItem("Formula2");
        formula2Item.Activated += (sender, e) => SpawnVehicle("Formula2");
        openWheelSubMenu.Add(formula2Item);

        NativeItem openWheel1Item = new NativeItem("OpenWheel1");
        openWheel1Item.Activated += (sender, e) => SpawnVehicle("OpenWheel1");
        openWheelSubMenu.Add(openWheel1Item);

        NativeItem openWheel2Item = new NativeItem("OpenWheel2");
        openWheel2Item.Activated += (sender, e) => SpawnVehicle("OpenWheel2");
        openWheelSubMenu.Add(openWheel2Item);

        // Planes

        NativeItem alkonostItem = new NativeItem("alkonost");
        alkonostItem.Activated += (sender, e) => SpawnVehicle("alkonost");
        planeSubMenu.Add(alkonostItem);

        NativeItem alphaz1Item = new NativeItem("alphaz1");
        alphaz1Item.Activated += (sender, e) => SpawnVehicle("alphaz1");
        planeSubMenu.Add(alphaz1Item);

        NativeItem avengerItem = new NativeItem("avenger");
        avengerItem.Activated += (sender, e) => SpawnVehicle("avenger");
        planeSubMenu.Add(avengerItem);

        NativeItem avenger2Item = new NativeItem("avenger2");
        avenger2Item.Activated += (sender, e) => SpawnVehicle("avenger2");
        planeSubMenu.Add(avenger2Item);

        NativeItem besraItem = new NativeItem("besra");
        besraItem.Activated += (sender, e) => SpawnVehicle("besra");
        planeSubMenu.Add(besraItem);

        NativeItem blimpItem = new NativeItem("BLIMP");
        blimpItem.Activated += (sender, e) => SpawnVehicle("BLIMP");
        planeSubMenu.Add(blimpItem);

        NativeItem blimp2Item = new NativeItem("BLIMP2");
        blimp2Item.Activated += (sender, e) => SpawnVehicle("BLIMP2");
        planeSubMenu.Add(blimp2Item);

        NativeItem blimp3Item = new NativeItem("blimp3");
        blimp3Item.Activated += (sender, e) => SpawnVehicle("blimp3");
        planeSubMenu.Add(blimp3Item);

        NativeItem bombushkaItem = new NativeItem("bombushka");
        bombushkaItem.Activated += (sender, e) => SpawnVehicle("bombushka");
        planeSubMenu.Add(bombushkaItem);

        NativeItem cargoplaneItem = new NativeItem("cargoplane");
        cargoplaneItem.Activated += (sender, e) => SpawnVehicle("cargoplane");
        planeSubMenu.Add(cargoplaneItem);

        NativeItem cargoplane2Item = new NativeItem("cargoplane2");
        cargoplane2Item.Activated += (sender, e) => SpawnVehicle("cargoplane2");
        planeSubMenu.Add(cargoplane2Item);

        NativeItem cuban800Item = new NativeItem("cuban800");
        cuban800Item.Activated += (sender, e) => SpawnVehicle("cuban800");
        planeSubMenu.Add(cuban800Item);

        NativeItem dodoItem = new NativeItem("dodo");
        dodoItem.Activated += (sender, e) => SpawnVehicle("dodo");
        planeSubMenu.Add(dodoItem);

        NativeItem dusterItem = new NativeItem("duster");
        dusterItem.Activated += (sender, e) => SpawnVehicle("duster");
        planeSubMenu.Add(dusterItem);

        NativeItem howardItem = new NativeItem("howard");
        howardItem.Activated += (sender, e) => SpawnVehicle("howard");
        planeSubMenu.Add(howardItem);

        NativeItem hydraItem = new NativeItem("hydra");
        hydraItem.Activated += (sender, e) => SpawnVehicle("hydra");
        planeSubMenu.Add(hydraItem);

        NativeItem jetItem = new NativeItem("jet");
        jetItem.Activated += (sender, e) => SpawnVehicle("jet");
        planeSubMenu.Add(jetItem);

        NativeItem lazerItem = new NativeItem("Lazer");
        lazerItem.Activated += (sender, e) => SpawnVehicle("Lazer");
        planeSubMenu.Add(lazerItem);

        NativeItem luxorItem = new NativeItem("luxor");
        luxorItem.Activated += (sender, e) => SpawnVehicle("luxor");
        planeSubMenu.Add(luxorItem);

        NativeItem luxor2Item = new NativeItem("luxor2");
        luxor2Item.Activated += (sender, e) => SpawnVehicle("luxor2");
        planeSubMenu.Add(luxor2Item);

        NativeItem mammatusItem = new NativeItem("mammatus");
        mammatusItem.Activated += (sender, e) => SpawnVehicle("mammatus");
        planeSubMenu.Add(mammatusItem);

        NativeItem microlightItem = new NativeItem("microlight");
        microlightItem.Activated += (sender, e) => SpawnVehicle("microlight");
        planeSubMenu.Add(microlightItem);

        NativeItem miljetItem = new NativeItem("Miljet");
        miljetItem.Activated += (sender, e) => SpawnVehicle("Miljet");
        planeSubMenu.Add(miljetItem);

        NativeItem mogulItem = new NativeItem("mogul");
        mogulItem.Activated += (sender, e) => SpawnVehicle("mogul");
        planeSubMenu.Add(mogulItem);

        NativeItem molotokItem = new NativeItem("molotok");
        molotokItem.Activated += (sender, e) => SpawnVehicle("molotok");
        planeSubMenu.Add(molotokItem);

        NativeItem nimbusItem = new NativeItem("nimbus");
        nimbusItem.Activated += (sender, e) => SpawnVehicle("nimbus");
        planeSubMenu.Add(nimbusItem);

        NativeItem nokotaItem = new NativeItem("nokota");
        nokotaItem.Activated += (sender, e) => SpawnVehicle("nokota");
        planeSubMenu.Add(nokotaItem);

        NativeItem pyroItem = new NativeItem("pyro");
        pyroItem.Activated += (sender, e) => SpawnVehicle("pyro");
        planeSubMenu.Add(pyroItem);

        NativeItem rogueItem = new NativeItem("rogue");
        rogueItem.Activated += (sender, e) => SpawnVehicle("rogue");
        planeSubMenu.Add(rogueItem);

        NativeItem seabreezeItem = new NativeItem("seabreeze");
        seabreezeItem.Activated += (sender, e) => SpawnVehicle("seabreeze");
        planeSubMenu.Add(seabreezeItem);

        NativeItem shamalItem = new NativeItem("Shamal");
        shamalItem.Activated += (sender, e) => SpawnVehicle("Shamal");
        planeSubMenu.Add(shamalItem);

        NativeItem starlingItem = new NativeItem("starling");
        starlingItem.Activated += (sender, e) => SpawnVehicle("starling");
        planeSubMenu.Add(starlingItem);

        NativeItem strikeforceItem = new NativeItem("strikeforce");
        strikeforceItem.Activated += (sender, e) => SpawnVehicle("strikeforce");
        planeSubMenu.Add(strikeforceItem);

        NativeItem StuntItem = new NativeItem("Stunt");
        StuntItem.Activated += (sender, e) => SpawnVehicle("Stunt");
        planeSubMenu.Add(StuntItem);

        NativeItem titanItem = new NativeItem("titan");
        titanItem.Activated += (sender, e) => SpawnVehicle("titan");
        planeSubMenu.Add(titanItem);

        NativeItem tulaItem = new NativeItem("tula");
        tulaItem.Activated += (sender, e) => SpawnVehicle("tula");
        planeSubMenu.Add(tulaItem);

        NativeItem velumItem = new NativeItem("velum");
        velumItem.Activated += (sender, e) => SpawnVehicle("velum");
        planeSubMenu.Add(velumItem);

        NativeItem velum2Item = new NativeItem("velum2");
        velum2Item.Activated += (sender, e) => SpawnVehicle("velum2");
        planeSubMenu.Add(velum2Item);

        NativeItem vestraItem = new NativeItem("vestra");
        vestraItem.Activated += (sender, e) => SpawnVehicle("vestra");
        planeSubMenu.Add(vestraItem);

        NativeItem volatolItem = new NativeItem("volatol");
        volatolItem.Activated += (sender, e) => SpawnVehicle("volatol");
        planeSubMenu.Add(volatolItem);

        // Rail

        NativeItem cablecarItem = new NativeItem("cablecar");
        cablecarItem.Activated += (sender, e) => SpawnVehicle("cablecar");
        railSubMenu.Add(cablecarItem);

        NativeItem freightItem = new NativeItem("freight");
        freightItem.Activated += (sender, e) => SpawnVehicle("freight");
        railSubMenu.Add(freightItem);

        NativeItem freightcarItem = new NativeItem("freightcar");
        freightcarItem.Activated += (sender, e) => SpawnVehicle("freightcar");
        railSubMenu.Add(freightcarItem);

        NativeItem freightcar2Item = new NativeItem("freightcar2");
        freightcar2Item.Activated += (sender, e) => SpawnVehicle("freightcar2");
        railSubMenu.Add(freightcar2Item);

        NativeItem freightcont1Item = new NativeItem("freightcont1");
        freightcont1Item.Activated += (sender, e) => SpawnVehicle("freightcont1");
        railSubMenu.Add(freightcont1Item);

        NativeItem freightcont2Item = new NativeItem("freightcont2");
        freightcont2Item.Activated += (sender, e) => SpawnVehicle("freightcont2");
        railSubMenu.Add(freightcont2Item);

        NativeItem freightgrainItem = new NativeItem("freightgrain");
        freightgrainItem.Activated += (sender, e) => SpawnVehicle("freightgrain");
        railSubMenu.Add(freightgrainItem);

        NativeItem metrotrainItem = new NativeItem("metrotrain");
        metrotrainItem.Activated += (sender, e) => SpawnVehicle("metrotrain");
        railSubMenu.Add(metrotrainItem);

        NativeItem tankercarItem = new NativeItem("tankercar");
        tankercarItem.Activated += (sender, e) => SpawnVehicle("tankercar");
        railSubMenu.Add(tankercarItem);

        // Sedans

        NativeItem aseaItem = new NativeItem("Asea");
        aseaItem.Activated += (sender, e) => SpawnVehicle("Asea");
        sedanSubMenu.Add(aseaItem);

        NativeItem asea2Item = new NativeItem("Asea");
        asea2Item.Activated += (sender, e) => SpawnVehicle("Asea");
        sedanSubMenu.Add(asea2Item);

        NativeItem asteropeItem = new NativeItem("Asterope");
        asteropeItem.Activated += (sender, e) => SpawnVehicle("Asterope");
        sedanSubMenu.Add(asteropeItem);

        NativeItem cinquemilaItem = new NativeItem("Cinquemila");
        cinquemilaItem.Activated += (sender, e) => SpawnVehicle("Cinquemila");
        sedanSubMenu.Add(cinquemilaItem);

        NativeItem cog55Item = new NativeItem("Cognoscenti 55");
        cog55Item.Activated += (sender, e) => SpawnVehicle("Cognoscenti 55");
        sedanSubMenu.Add(cog55Item);

        NativeItem cog552Item = new NativeItem("Cognoscenti 55 (Armored)");
        cog552Item.Activated += (sender, e) => SpawnVehicle("Cognoscenti 55 (Armored)");
        sedanSubMenu.Add(cog552Item);

        NativeItem cognoscentiItem = new NativeItem("Cognoscenti");
        cognoscentiItem.Activated += (sender, e) => SpawnVehicle("Cognoscenti");
        sedanSubMenu.Add(cognoscentiItem);

        NativeItem cognoscenti2Item = new NativeItem("Cognoscenti (Armored)");
        cognoscenti2Item.Activated += (sender, e) => SpawnVehicle("Cognoscenti (Armored)");
        sedanSubMenu.Add(cognoscenti2Item);

        NativeItem deityItem = new NativeItem("Deity");
        deityItem.Activated += (sender, e) => SpawnVehicle("Deity");
        sedanSubMenu.Add(deityItem);

        NativeItem emperorItem = new NativeItem("Emperor");
        emperorItem.Activated += (sender, e) => SpawnVehicle("Emperor");
        sedanSubMenu.Add(emperorItem);

        NativeItem emperor2Item = new NativeItem("Emperor");
        emperor2Item.Activated += (sender, e) => SpawnVehicle("Emperor");
        sedanSubMenu.Add(emperor2Item);

        NativeItem emperor3Item = new NativeItem("Emperor");
        emperor3Item.Activated += (sender, e) => SpawnVehicle("Emperor");
        sedanSubMenu.Add(emperor3Item);

        NativeItem fugitiveItem = new NativeItem("Fugitive");
        fugitiveItem.Activated += (sender, e) => SpawnVehicle("Fugitive");
        sedanSubMenu.Add(fugitiveItem);

        NativeItem glendaleItem = new NativeItem("Glendale");
        glendaleItem.Activated += (sender, e) => SpawnVehicle("Glendale");
        sedanSubMenu.Add(glendaleItem);

        NativeItem glendale2Item = new NativeItem("Glendale Custom");
        glendale2Item.Activated += (sender, e) => SpawnVehicle("Glendale Custom");
        sedanSubMenu.Add(glendale2Item);

        NativeItem ingotItem = new NativeItem("Ingot");
        ingotItem.Activated += (sender, e) => SpawnVehicle("Ingot");
        sedanSubMenu.Add(ingotItem);

        NativeItem intruderItem = new NativeItem("Intruder");
        intruderItem.Activated += (sender, e) => SpawnVehicle("Intruder");
        sedanSubMenu.Add(intruderItem);

        NativeItem limo2Item = new NativeItem("Turreted Limo");
        limo2Item.Activated += (sender, e) => SpawnVehicle("Turreted Limo");
        sedanSubMenu.Add(limo2Item);

        NativeItem premierItem = new NativeItem("Premier");
        premierItem.Activated += (sender, e) => SpawnVehicle("Premier");
        sedanSubMenu.Add(premierItem);

        NativeItem primoItem = new NativeItem("Primo");
        primoItem.Activated += (sender, e) => SpawnVehicle("Primo");
        sedanSubMenu.Add(primoItem);

        NativeItem primo2Item = new NativeItem("Primo Custom");
        primo2Item.Activated += (sender, e) => SpawnVehicle("Primo Custom");
        sedanSubMenu.Add(primo2Item);

        NativeItem reginaItem = new NativeItem("Regina");
        reginaItem.Activated += (sender, e) => SpawnVehicle("Regina");
        sedanSubMenu.Add(reginaItem);

        NativeItem rhinehartItem = new NativeItem("Rhinehart");
        rhinehartItem.Activated += (sender, e) => SpawnVehicle("Rhinehart");
        sedanSubMenu.Add(rhinehartItem);

        NativeItem romeroItem = new NativeItem("Romero Hearse");
        romeroItem.Activated += (sender, e) => SpawnVehicle("Romero Hearse");
        sedanSubMenu.Add(romeroItem);

        NativeItem schafter2Item = new NativeItem("Schafter");
        schafter2Item.Activated += (sender, e) => SpawnVehicle("Schafter");
        sedanSubMenu.Add(schafter2Item);

        NativeItem schafter5Item = new NativeItem("Schafter V12 (Armored)");
        schafter5Item.Activated += (sender, e) => SpawnVehicle("Schafter V12 (Armored)");
        sedanSubMenu.Add(schafter5Item);

        NativeItem schafter6Item = new NativeItem("Schafter LWB (Armored)");
        schafter6Item.Activated += (sender, e) => SpawnVehicle("Schafter LWB (Armored)");
        sedanSubMenu.Add(schafter6Item);

        NativeItem staffordItem = new NativeItem("Stafford");
        staffordItem.Activated += (sender, e) => SpawnVehicle("Stafford");
        sedanSubMenu.Add(staffordItem);

        NativeItem stanierItem = new NativeItem("Stanier");
        stanierItem.Activated += (sender, e) => SpawnVehicle("Stanier");
        sedanSubMenu.Add(stanierItem);

        NativeItem stratumItem = new NativeItem("Stratum");
        stratumItem.Activated += (sender, e) => SpawnVehicle("Stratum");
        sedanSubMenu.Add(stratumItem);

        NativeItem stretchItem = new NativeItem("Stretch");
        stretchItem.Activated += (sender, e) => SpawnVehicle("Stretch");
        sedanSubMenu.Add(stretchItem);

        NativeItem superdItem = new NativeItem("Super Diamond");
        superdItem.Activated += (sender, e) => SpawnVehicle("Super Diamond");
        sedanSubMenu.Add(superdItem);

        NativeItem surgeItem = new NativeItem("Surge");
        surgeItem.Activated += (sender, e) => SpawnVehicle("Surge");
        sedanSubMenu.Add(surgeItem);

        NativeItem tailgaterItem = new NativeItem("Tailgater");
        tailgaterItem.Activated += (sender, e) => SpawnVehicle("Tailgater");
        sedanSubMenu.Add(tailgaterItem);

        NativeItem tailgater2Item = new NativeItem("Tailgater S");
        tailgater2Item.Activated += (sender, e) => SpawnVehicle("Tailgater S");
        sedanSubMenu.Add(tailgater2Item);

        NativeItem warrenerItem = new NativeItem("Warrener");
        warrenerItem.Activated += (sender, e) => SpawnVehicle("Warrener");
        sedanSubMenu.Add(warrenerItem);

        NativeItem warrener2Item = new NativeItem("Warrener HKR");
        warrener2Item.Activated += (sender, e) => SpawnVehicle("Warrener HKR");
        sedanSubMenu.Add(warrener2Item);

        NativeItem washingtonItem = new NativeItem("Washington");
        washingtonItem.Activated += (sender, e) => SpawnVehicle("Washington");
        sedanSubMenu.Add(washingtonItem);

        // Service

        NativeItem airbusItem = new NativeItem("Airbus");
        airbusItem.Activated += (sender, e) => SpawnVehicle("Airbus");
        serviceSubMenu.Add(airbusItem);

        NativeItem brickadeItem = new NativeItem("Brickade");
        brickadeItem.Activated += (sender, e) => SpawnVehicle("Brickade");
        serviceSubMenu.Add(brickadeItem);

        NativeItem brickade2Item = new NativeItem("Brickade 6x6");
        brickade2Item.Activated += (sender, e) => SpawnVehicle("Brickade 6x6");
        serviceSubMenu.Add(brickade2Item);

        NativeItem busItem = new NativeItem("Bus");
        busItem.Activated += (sender, e) => SpawnVehicle("Bus");
        serviceSubMenu.Add(busItem);

        NativeItem coachItem = new NativeItem("Dashound");
        coachItem.Activated += (sender, e) => SpawnVehicle("Dashound");
        serviceSubMenu.Add(coachItem);

        NativeItem pbus2Item = new NativeItem("Festival Bus");
        pbus2Item.Activated += (sender, e) => SpawnVehicle("Festival Bus");
        serviceSubMenu.Add(pbus2Item);

        NativeItem rallytruckItem = new NativeItem("Dune");
        rallytruckItem.Activated += (sender, e) => SpawnVehicle("Dune");
        serviceSubMenu.Add(rallytruckItem);

        NativeItem rentalbusItem = new NativeItem("Rental Shuttle Bus");
        rentalbusItem.Activated += (sender, e) => SpawnVehicle("Rental Shuttle Bus");
        serviceSubMenu.Add(rentalbusItem);

        NativeItem taxiItem = new NativeItem("Taxi");
        taxiItem.Activated += (sender, e) => SpawnVehicle("Taxi");
        serviceSubMenu.Add(taxiItem);

        NativeItem tourbusItem = new NativeItem("Tourbus");
        tourbusItem.Activated += (sender, e) => SpawnVehicle("Tourbus");
        serviceSubMenu.Add(tourbusItem);

        NativeItem trashItem = new NativeItem("Trashmaster");
        trashItem.Activated += (sender, e) => SpawnVehicle("Trashmaster");
        serviceSubMenu.Add(trashItem);

        NativeItem trash2Item = new NativeItem("Trashmaster");
        trash2Item.Activated += (sender, e) => SpawnVehicle("Trashmaster");
        serviceSubMenu.Add(trash2Item);

        NativeItem wastelanderItem = new NativeItem("Wastelander");
        wastelanderItem.Activated += (sender, e) => SpawnVehicle("Wastelander");
        serviceSubMenu.Add(wastelanderItem);

        // Sports

        NativeItem alphaItem = new NativeItem("Alpha");
        alphaItem.Activated += (sender, e) => SpawnVehicle("alpha");
        sportsSubMenu.Add(alphaItem);

        NativeItem bansheeItem = new NativeItem("Banshee");
        bansheeItem.Activated += (sender, e) => SpawnVehicle("banshee");
        sportsSubMenu.Add(bansheeItem);

        NativeItem bestiaGTSItem = new NativeItem("Bestia GTS");
        bestiaGTSItem.Activated += (sender, e) => SpawnVehicle("bestiagts");
        sportsSubMenu.Add(bestiaGTSItem);

        NativeItem blistaCompactItem = new NativeItem("Blista Compact");
        blistaCompactItem.Activated += (sender, e) => SpawnVehicle("blista2");
        sportsSubMenu.Add(blistaCompactItem);

        NativeItem goGoMonkeyBlistaItem = new NativeItem("Go Go Monkey Blista");
        goGoMonkeyBlistaItem.Activated += (sender, e) => SpawnVehicle("blista3");
        sportsSubMenu.Add(goGoMonkeyBlistaItem);

        NativeItem buffaloItem = new NativeItem("Buffalo");
        buffaloItem.Activated += (sender, e) => SpawnVehicle("buffalo");
        sportsSubMenu.Add(buffaloItem);

        NativeItem buffaloSItem = new NativeItem("Buffalo S");
        buffaloSItem.Activated += (sender, e) => SpawnVehicle("buffalo2");
        sportsSubMenu.Add(buffaloSItem);

        NativeItem sprunkBuffaloItem = new NativeItem("Sprunk Buffalo");
        sprunkBuffaloItem.Activated += (sender, e) => SpawnVehicle("buffalo3");
        sportsSubMenu.Add(sprunkBuffaloItem);

        NativeItem calicoGTFItem = new NativeItem("Calico GTF");
        calicoGTFItem.Activated += (sender, e) => SpawnVehicle("calico");
        sportsSubMenu.Add(calicoGTFItem);

        NativeItem carbonizzareItem = new NativeItem("Carbonizzare");
        carbonizzareItem.Activated += (sender, e) => SpawnVehicle("carbonizzare");
        sportsSubMenu.Add(carbonizzareItem);

        NativeItem cometItem = new NativeItem("Comet");
        cometItem.Activated += (sender, e) => SpawnVehicle("comet2");
        sportsSubMenu.Add(cometItem);

        NativeItem cometRetroCustomItem = new NativeItem("Comet Retro Custom");
        cometRetroCustomItem.Activated += (sender, e) => SpawnVehicle("comet3");
        sportsSubMenu.Add(cometRetroCustomItem);

        NativeItem cometSafariItem = new NativeItem("Comet Safari");
        cometSafariItem.Activated += (sender, e) => SpawnVehicle("comet4");
        sportsSubMenu.Add(cometSafariItem);

        NativeItem cometSRItem = new NativeItem("Comet SR");
        cometSRItem.Activated += (sender, e) => SpawnVehicle("comet5");
        sportsSubMenu.Add(cometSRItem);

        NativeItem cometS2Item = new NativeItem("Comet S2");
        cometS2Item.Activated += (sender, e) => SpawnVehicle("comet6");
        sportsSubMenu.Add(cometS2Item);

        NativeItem cometS2CabrioItem = new NativeItem("Comet S2 Cabrio");
        cometS2CabrioItem.Activated += (sender, e) => SpawnVehicle("comet7");
        sportsSubMenu.Add(cometS2CabrioItem);

        NativeItem coquetteItem = new NativeItem("Coquette");
        coquetteItem.Activated += (sender, e) => SpawnVehicle("coquette");
        sportsSubMenu.Add(coquetteItem);

        NativeItem coquetteD10Item = new NativeItem("Coquette D10");
        coquetteD10Item.Activated += (sender, e) => SpawnVehicle("coquette4");
        sportsSubMenu.Add(coquetteD10Item);

        NativeItem corsitaItem = new NativeItem("Corsita");
        corsitaItem.Activated += (sender, e) => SpawnVehicle("corsita");
        sportsSubMenu.Add(corsitaItem);

        NativeItem cypherItem = new NativeItem("Cypher");
        cypherItem.Activated += (sender, e) => SpawnVehicle("cypher");
        sportsSubMenu.Add(cypherItem);

        NativeItem drafterItem = new NativeItem("8F Drafter");
        drafterItem.Activated += (sender, e) => SpawnVehicle("drafter");
        sportsSubMenu.Add(drafterItem);

        NativeItem elegyRetroCustomItem = new NativeItem("Elegy Retro Custom");
        elegyRetroCustomItem.Activated += (sender, e) => SpawnVehicle("elegy");
        sportsSubMenu.Add(elegyRetroCustomItem);

        NativeItem elegyRH8Item = new NativeItem("Elegy RH8");
        elegyRH8Item.Activated += (sender, e) => SpawnVehicle("elegy2");
        sportsSubMenu.Add(elegyRH8Item);

        NativeItem eurosItem = new NativeItem("Euros");
        eurosItem.Activated += (sender, e) => SpawnVehicle("Euros");
        sportsSubMenu.Add(eurosItem);

        NativeItem hotringEveronItem = new NativeItem("Hotring Everon");
        hotringEveronItem.Activated += (sender, e) => SpawnVehicle("everon2");
        sportsSubMenu.Add(hotringEveronItem);

        NativeItem feltzerItem = new NativeItem("Feltzer");
        feltzerItem.Activated += (sender, e) => SpawnVehicle("feltzer2");
        sportsSubMenu.Add(feltzerItem);

        NativeItem flashGTItem = new NativeItem("Flash GT");
        flashGTItem.Activated += (sender, e) => SpawnVehicle("flashgt");
        sportsSubMenu.Add(flashGTItem);

        NativeItem furoreGTItem = new NativeItem("Furore GT");
        furoreGTItem.Activated += (sender, e) => SpawnVehicle("furoregt");
        sportsSubMenu.Add(furoreGTItem);

        NativeItem fusiladeItem = new NativeItem("Fusilade");
        fusiladeItem.Activated += (sender, e) => SpawnVehicle("fusilade");
        sportsSubMenu.Add(fusiladeItem);

        NativeItem futoItem = new NativeItem("Futo");
        futoItem.Activated += (sender, e) => SpawnVehicle("futo");
        sportsSubMenu.Add(futoItem);

        NativeItem futoGTXItem = new NativeItem("Futo GTX");
        futoGTXItem.Activated += (sender, e) => SpawnVehicle("futo2");
        sportsSubMenu.Add(futoGTXItem);

        NativeItem gb200Item = new NativeItem("GB200");
        gb200Item.Activated += (sender, e) => SpawnVehicle("gb200");
        sportsSubMenu.Add(gb200Item);

        NativeItem growlerItem = new NativeItem("Growler");
        growlerItem.Activated += (sender, e) => SpawnVehicle("growler");
        sportsSubMenu.Add(growlerItem);

        NativeItem hotringSabreItem = new NativeItem("Hotring Sabre");
        hotringSabreItem.Activated += (sender, e) => SpawnVehicle("hotring");
        sportsSubMenu.Add(hotringSabreItem);

        NativeItem imorgonItem = new NativeItem("Imorgon");
        imorgonItem.Activated += (sender, e) => SpawnVehicle("imorgon");
        sportsSubMenu.Add(imorgonItem);

        NativeItem issiSportItem = new NativeItem("Issi Sport");
        issiSportItem.Activated += (sender, e) => SpawnVehicle("issi7");
        sportsSubMenu.Add(issiSportItem);

        NativeItem italiGTOItem = new NativeItem("Itali GTO");
        italiGTOItem.Activated += (sender, e) => SpawnVehicle("italigto");
        sportsSubMenu.Add(italiGTOItem);

        NativeItem italiRSXItem = new NativeItem("Itali RSX");
        italiRSXItem.Activated += (sender, e) => SpawnVehicle("italirsx");
        sportsSubMenu.Add(italiRSXItem);

        NativeItem jesterItem = new NativeItem("Jester");
        jesterItem.Activated += (sender, e) => SpawnVehicle("jester");
        sportsSubMenu.Add(jesterItem);

        NativeItem jesterRacecarItem = new NativeItem("Jester (Racecar)");
        jesterRacecarItem.Activated += (sender, e) => SpawnVehicle("jester2");
        sportsSubMenu.Add(jesterRacecarItem);

        NativeItem jesterClassicItem = new NativeItem("Jester Classic");
        jesterClassicItem.Activated += (sender, e) => SpawnVehicle("jester3");
        sportsSubMenu.Add(jesterClassicItem);

        NativeItem jesterRRItem = new NativeItem("Jester RR");
        jesterRRItem.Activated += (sender, e) => SpawnVehicle("jester4");
        sportsSubMenu.Add(jesterRRItem);

        NativeItem jugularItem = new NativeItem("Jugular");
        jugularItem.Activated += (sender, e) => SpawnVehicle("jugular");
        sportsSubMenu.Add(jugularItem);

        NativeItem khamelionItem = new NativeItem("Khamelion");
        khamelionItem.Activated += (sender, e) => SpawnVehicle("khamelion");
        sportsSubMenu.Add(khamelionItem);

        NativeItem komodaItem = new NativeItem("Komoda");
        komodaItem.Activated += (sender, e) => SpawnVehicle("komoda");
        sportsSubMenu.Add(komodaItem);

        NativeItem kurumaItem = new NativeItem("Kuruma");
        kurumaItem.Activated += (sender, e) => SpawnVehicle("kuruma");
        sportsSubMenu.Add(kurumaItem);

        NativeItem armoredKurumaItem = new NativeItem("Kuruma (Armored)");
        armoredKurumaItem.Activated += (sender, e) => SpawnVehicle("kuruma2");
        sportsSubMenu.Add(armoredKurumaItem);

        NativeItem locustItem = new NativeItem("Locust");
        locustItem.Activated += (sender, e) => SpawnVehicle("locust");
        sportsSubMenu.Add(locustItem);

        NativeItem lynxItem = new NativeItem("Lynx");
        lynxItem.Activated += (sender, e) => SpawnVehicle("lynx");
        sportsSubMenu.Add(lynxItem);

        NativeItem massacroItem = new NativeItem("Massacro");
        massacroItem.Activated += (sender, e) => SpawnVehicle("massacro");
        sportsSubMenu.Add(massacroItem);

        NativeItem massacroRacecarItem = new NativeItem("Massacro (Racecar)");
        massacroRacecarItem.Activated += (sender, e) => SpawnVehicle("massacro2");
        sportsSubMenu.Add(massacroRacecarItem);

        NativeItem neoItem = new NativeItem("Neo");
        neoItem.Activated += (sender, e) => SpawnVehicle("neo");
        sportsSubMenu.Add(neoItem);

        NativeItem neonItem = new NativeItem("Neon");
        neonItem.Activated += (sender, e) => SpawnVehicle("neon");
        sportsSubMenu.Add(neonItem);

        NativeItem ninefItem = new NativeItem("9F");
        ninefItem.Activated += (sender, e) => SpawnVehicle("ninef");
        sportsSubMenu.Add(ninefItem);

        NativeItem ninefCabrioItem = new NativeItem("9F Cabrio");
        ninefCabrioItem.Activated += (sender, e) => SpawnVehicle("ninef2");
        sportsSubMenu.Add(ninefCabrioItem);

        NativeItem omnisItem = new NativeItem("Omnis");
        omnisItem.Activated += (sender, e) => SpawnVehicle("omnis");
        sportsSubMenu.Add(omnisItem);

        NativeItem omnisEGTItem = new NativeItem("Omnis e-GT");
        omnisEGTItem.Activated += (sender, e) => SpawnVehicle("omnisegt");
        sportsSubMenu.Add(omnisEGTItem);

        NativeItem panthereItem = new NativeItem("Panthere");
        panthereItem.Activated += (sender, e) => SpawnVehicle("panthere");
        sportsSubMenu.Add(panthereItem);

        NativeItem paragonItem = new NativeItem("Paragon R");
        paragonItem.Activated += (sender, e) => SpawnVehicle("paragon");
        sportsSubMenu.Add(paragonItem);

        NativeItem armoredParagonItem = new NativeItem("Paragon R (Armored)");
        armoredParagonItem.Activated += (sender, e) => SpawnVehicle("paragon2");
        sportsSubMenu.Add(armoredParagonItem);

        NativeItem pariahItem = new NativeItem("Pariah");
        pariahItem.Activated += (sender, e) => SpawnVehicle("pariah");
        sportsSubMenu.Add(pariahItem);

        NativeItem penumbraItem = new NativeItem("Penumbra");
        penumbraItem.Activated += (sender, e) => SpawnVehicle("penumbra");
        sportsSubMenu.Add(penumbraItem);

        NativeItem penumbraFFItem = new NativeItem("Penumbra FF");
        penumbraFFItem.Activated += (sender, e) => SpawnVehicle("penumbra2");
        sportsSubMenu.Add(penumbraFFItem);

        NativeItem r300Item = new NativeItem("300R");
        r300Item.Activated += (sender, e) => SpawnVehicle("r300");
        sportsSubMenu.Add(r300Item);

        NativeItem raidenItem = new NativeItem("Raiden");
        raidenItem.Activated += (sender, e) => SpawnVehicle("raiden");
        sportsSubMenu.Add(raidenItem);

        NativeItem rapidGTItem = new NativeItem("Rapid GT");
        rapidGTItem.Activated += (sender, e) => SpawnVehicle("RapidGT");
        sportsSubMenu.Add(rapidGTItem);

        NativeItem rapidGT2Item = new NativeItem("Rapid GT");
        rapidGT2Item.Activated += (sender, e) => SpawnVehicle("RapidGT2");
        sportsSubMenu.Add(rapidGT2Item);

        NativeItem raptorItem = new NativeItem("Raptor");
        raptorItem.Activated += (sender, e) => SpawnVehicle("raptor");
        sportsSubMenu.Add(raptorItem);

        NativeItem remusItem = new NativeItem("Remus");
        remusItem.Activated += (sender, e) => SpawnVehicle("remus");
        sportsSubMenu.Add(remusItem);

        NativeItem revolterItem = new NativeItem("Revolter");
        revolterItem.Activated += (sender, e) => SpawnVehicle("revolter");
        sportsSubMenu.Add(revolterItem);

        NativeItem rt3000Item = new NativeItem("RT3000");
        rt3000Item.Activated += (sender, e) => SpawnVehicle("rt3000");
        sportsSubMenu.Add(rt3000Item);

        NativeItem rustonItem = new NativeItem("Ruston");
        rustonItem.Activated += (sender, e) => SpawnVehicle("ruston");
        sportsSubMenu.Add(rustonItem);

        NativeItem schafterV12Item = new NativeItem("Schafter V12");
        schafterV12Item.Activated += (sender, e) => SpawnVehicle("schafter3");
        sportsSubMenu.Add(schafterV12Item);

        NativeItem schafterLWBItem = new NativeItem("Schafter LWB");
        schafterLWBItem.Activated += (sender, e) => SpawnVehicle("schafter4");
        sportsSubMenu.Add(schafterLWBItem);

        NativeItem schlagenGTItem = new NativeItem("Schlagen GT");
        schlagenGTItem.Activated += (sender, e) => SpawnVehicle("schlagen");
        sportsSubMenu.Add(schlagenGTItem);

        NativeItem schwarzerItem = new NativeItem("Schwartzer");
        schwarzerItem.Activated += (sender, e) => SpawnVehicle("schwarzer");
        sportsSubMenu.Add(schwarzerItem);

        NativeItem sentinel1Item = new NativeItem("Sentinel");
        sentinel1Item.Activated += (sender, e) => SpawnVehicle("sentinel3");
        sportsSubMenu.Add(sentinel1Item);

        NativeItem sentinelClassicWidebodyItem = new NativeItem("Sentinel Classic Widebody");
        sentinelClassicWidebodyItem.Activated += (sender, e) => SpawnVehicle("sentinel4");
        sportsSubMenu.Add(sentinelClassicWidebodyItem);

        NativeItem seven70Item = new NativeItem("Seven-70");
        seven70Item.Activated += (sender, e) => SpawnVehicle("SEVEN70");
        sportsSubMenu.Add(seven70Item);

        NativeItem sm722Item = new NativeItem("SM722");
        sm722Item.Activated += (sender, e) => SpawnVehicle("sm722");
        sportsSubMenu.Add(sm722Item);

        NativeItem specterItem = new NativeItem("Specter");
        specterItem.Activated += (sender, e) => SpawnVehicle("SPECTER");
        sportsSubMenu.Add(specterItem);

        NativeItem specterCustomItem = new NativeItem("Specter Custom");
        specterCustomItem.Activated += (sender, e) => SpawnVehicle("SPECTER2");
        sportsSubMenu.Add(specterCustomItem);

        NativeItem streiterItem = new NativeItem("Streiter");
        streiterItem.Activated += (sender, e) => SpawnVehicle("streiter");
        sportsSubMenu.Add(streiterItem);

        NativeItem sugoiItem = new NativeItem("Sugoi");
        sugoiItem.Activated += (sender, e) => SpawnVehicle("Sugoi");
        sportsSubMenu.Add(sugoiItem);

        NativeItem sultanItem = new NativeItem("Sultan");
        sultanItem.Activated += (sender, e) => SpawnVehicle("sultan");
        sportsSubMenu.Add(sultanItem);

        NativeItem sultanClassicItem = new NativeItem("Sultan Classic");
        sultanClassicItem.Activated += (sender, e) => SpawnVehicle("sultan2");
        sportsSubMenu.Add(sultanClassicItem);

        NativeItem sultanRSClassicItem = new NativeItem("Sultan RS Classic");
        sultanRSClassicItem.Activated += (sender, e) => SpawnVehicle("sultan3");
        sportsSubMenu.Add(sultanRSClassicItem);

        NativeItem suranoItem = new NativeItem("Surano");
        suranoItem.Activated += (sender, e) => SpawnVehicle("Surano");
        sportsSubMenu.Add(suranoItem);

        NativeItem driftTampaItem = new NativeItem("Drift Tampa");
        driftTampaItem.Activated += (sender, e) => SpawnVehicle("tampa2");
        sportsSubMenu.Add(driftTampaItem);

        NativeItem tenfItem = new NativeItem("10F");
        tenfItem.Activated += (sender, e) => SpawnVehicle("tenf");
        sportsSubMenu.Add(tenfItem);

        NativeItem tenfWidebodyItem = new NativeItem("10F Widebody");
        tenfWidebodyItem.Activated += (sender, e) => SpawnVehicle("tenf2");
        sportsSubMenu.Add(tenfWidebodyItem);

        NativeItem troposRallyeItem = new NativeItem("Tropos Rallye");
        troposRallyeItem.Activated += (sender, e) => SpawnVehicle("tropos");
        sportsSubMenu.Add(troposRallyeItem);

        NativeItem vectreItem = new NativeItem("Vectre");
        vectreItem.Activated += (sender, e) => SpawnVehicle("vectre");
        sportsSubMenu.Add(vectreItem);

        NativeItem verliererItem = new NativeItem("Verlierer");
        verliererItem.Activated += (sender, e) => SpawnVehicle("verlierer2");
        sportsSubMenu.Add(verliererItem);

        NativeItem vetoClassicItem = new NativeItem("Veto Classic");
        vetoClassicItem.Activated += (sender, e) => SpawnVehicle("veto");
        sportsSubMenu.Add(vetoClassicItem);

        NativeItem vetoModernItem = new NativeItem("Veto Modern");
        vetoModernItem.Activated += (sender, e) => SpawnVehicle("veto2");
        sportsSubMenu.Add(vetoModernItem);

        NativeItem vstrItem = new NativeItem("V-STR");
        vstrItem.Activated += (sender, e) => SpawnVehicle("vstr");
        sportsSubMenu.Add(vstrItem);

        NativeItem zr350Item = new NativeItem("ZR350");
        zr350Item.Activated += (sender, e) => SpawnVehicle("zr350");
        sportsSubMenu.Add(zr350Item);

        NativeItem apocalypseZR380Item = new NativeItem("Apocalypse ZR380");
        apocalypseZR380Item.Activated += (sender, e) => SpawnVehicle("zr380");
        sportsSubMenu.Add(apocalypseZR380Item);

        NativeItem futureShockZR380Item = new NativeItem("Future Shock ZR380");
        futureShockZR380Item.Activated += (sender, e) => SpawnVehicle("zr3802");
        sportsSubMenu.Add(futureShockZR380Item);

        NativeItem nightmareZR380Item = new NativeItem("Nightmare ZR380");
        nightmareZR380Item.Activated += (sender, e) => SpawnVehicle("zr3803");
        sportsSubMenu.Add(nightmareZR380Item);

        // Sports Classic

        NativeItem ardentItem = new NativeItem("Ardent");
        ardentItem.Activated += (sender, e) => SpawnVehicle("ardent");
        sportsClassicSubMenu.Add(ardentItem);

        NativeItem rooseveltItem = new NativeItem("Roosevelt");
        rooseveltItem.Activated += (sender, e) => SpawnVehicle("roosevelt");
        sportsClassicSubMenu.Add(rooseveltItem);

        NativeItem frankenStangeItem = new NativeItem("Fränken Stange");
        frankenStangeItem.Activated += (sender, e) => SpawnVehicle("frankenstange");
        sportsClassicSubMenu.Add(frankenStangeItem);

        NativeItem rooseveltValorItem = new NativeItem("Roosevelt Valor");
        rooseveltValorItem.Activated += (sender, e) => SpawnVehicle("rooseveltvalor");
        sportsClassicSubMenu.Add(rooseveltValorItem);

        NativeItem cascoItem = new NativeItem("Casco");
        cascoItem.Activated += (sender, e) => SpawnVehicle("casco");
        sportsClassicSubMenu.Add(cascoItem);

        NativeItem cheburekItem = new NativeItem("Cheburek");
        cheburekItem.Activated += (sender, e) => SpawnVehicle("cheburek");
        sportsClassicSubMenu.Add(cheburekItem);

        NativeItem cheetahClassicItem = new NativeItem("Cheetah Classic");
        cheetahClassicItem.Activated += (sender, e) => SpawnVehicle("cheetahclassic");
        sportsClassicSubMenu.Add(cheetahClassicItem);

        NativeItem coquetteClassicItem = new NativeItem("Coquette Classic");
        coquetteClassicItem.Activated += (sender, e) => SpawnVehicle("coquetteclassic");
        sportsClassicSubMenu.Add(coquetteClassicItem);

        NativeItem deluxoItem = new NativeItem("Deluxo");
        deluxoItem.Activated += (sender, e) => SpawnVehicle("deluxo");
        sportsClassicSubMenu.Add(deluxoItem);

        NativeItem dynastyItem = new NativeItem("Dynasty");
        dynastyItem.Activated += (sender, e) => SpawnVehicle("dynasty");
        sportsClassicSubMenu.Add(dynastyItem);

        NativeItem fagaloaItem = new NativeItem("Fagaloa");
        fagaloaItem.Activated += (sender, e) => SpawnVehicle("fagaloa");
        sportsClassicSubMenu.Add(fagaloaItem);

        NativeItem stirlingGTItem = new NativeItem("Stirling GT");
        stirlingGTItem.Activated += (sender, e) => SpawnVehicle("stirlinggt");
        sportsClassicSubMenu.Add(stirlingGTItem);

        NativeItem gt500Item = new NativeItem("GT500");
        gt500Item.Activated += (sender, e) => SpawnVehicle("gt500");
        sportsClassicSubMenu.Add(gt500Item);

        NativeItem infernusClassicItem = new NativeItem("Infernus Classic");
        infernusClassicItem.Activated += (sender, e) => SpawnVehicle("infernusclassic");
        sportsClassicSubMenu.Add(infernusClassicItem);

        NativeItem jb700Item = new NativeItem("JB 700");
        jb700Item.Activated += (sender, e) => SpawnVehicle("jb700");
        sportsClassicSubMenu.Add(jb700Item);

        NativeItem jb700WItem = new NativeItem("JB 700W");
        jb700WItem.Activated += (sender, e) => SpawnVehicle("jb700w");
        sportsClassicSubMenu.Add(jb700WItem);

        NativeItem mambaItem = new NativeItem("Mamba");
        mambaItem.Activated += (sender, e) => SpawnVehicle("mamba");
        sportsClassicSubMenu.Add(mambaItem);

        NativeItem mananaItem = new NativeItem("Manana");
        mananaItem.Activated += (sender, e) => SpawnVehicle("manana");
        sportsClassicSubMenu.Add(mananaItem);

        NativeItem michelliGTItem = new NativeItem("Michelli GT");
        michelliGTItem.Activated += (sender, e) => SpawnVehicle("micheligt");
        sportsClassicSubMenu.Add(michelliGTItem);

        NativeItem monroeItem = new NativeItem("Monroe");
        monroeItem.Activated += (sender, e) => SpawnVehicle("monroe");
        sportsClassicSubMenu.Add(monroeItem);

        NativeItem nebulaTurboItem = new NativeItem("Nebula Turbo");
        nebulaTurboItem.Activated += (sender, e) => SpawnVehicle("nebulaturbo");
        sportsClassicSubMenu.Add(nebulaTurboItem);

        NativeItem peyoteItem = new NativeItem("Peyote");
        peyoteItem.Activated += (sender, e) => SpawnVehicle("peyote");
        sportsClassicSubMenu.Add(peyoteItem);

        NativeItem peyoteCustomItem = new NativeItem("Peyote Custom");
        peyoteCustomItem.Activated += (sender, e) => SpawnVehicle("peyotecustom");
        sportsClassicSubMenu.Add(peyoteCustomItem);

        NativeItem pigalleItem = new NativeItem("Pigalle");
        pigalleItem.Activated += (sender, e) => SpawnVehicle("pigalle");
        sportsClassicSubMenu.Add(pigalleItem);

        NativeItem rapidGTClassicItem = new NativeItem("Rapid GT Classic");
        rapidGTClassicItem.Activated += (sender, e) => SpawnVehicle("rapidgtclassic");
        sportsClassicSubMenu.Add(rapidGTClassicItem);

        NativeItem retinueItem = new NativeItem("Retinue");
        retinueItem.Activated += (sender, e) => SpawnVehicle("retinue");
        sportsClassicSubMenu.Add(retinueItem);

        NativeItem retinueMkIIItem = new NativeItem("Retinue Mk II");
        retinueMkIIItem.Activated += (sender, e) => SpawnVehicle("retinuemk2");
        sportsClassicSubMenu.Add(retinueMkIIItem);

        NativeItem savestraItem = new NativeItem("Savestra");
        savestraItem.Activated += (sender, e) => SpawnVehicle("savestra");
        sportsClassicSubMenu.Add(savestraItem);

        NativeItem stingerItem = new NativeItem("Stinger");
        stingerItem.Activated += (sender, e) => SpawnVehicle("stinger");
        sportsClassicSubMenu.Add(stingerItem);

        NativeItem stingerGTItem = new NativeItem("Stinger GT");
        stingerGTItem.Activated += (sender, e) => SpawnVehicle("stingergt");
        sportsClassicSubMenu.Add(stingerGTItem);

        NativeItem strombergItem = new NativeItem("Stromberg");
        strombergItem.Activated += (sender, e) => SpawnVehicle("stromberg");
        sportsClassicSubMenu.Add(strombergItem);

        NativeItem swingerItem = new NativeItem("Swinger");
        swingerItem.Activated += (sender, e) => SpawnVehicle("swinger");
        sportsClassicSubMenu.Add(swingerItem);

        NativeItem toreadorItem = new NativeItem("Toreador");
        toreadorItem.Activated += (sender, e) => SpawnVehicle("toreador");
        sportsClassicSubMenu.Add(toreadorItem);

        NativeItem toreroItem = new NativeItem("Torero");
        toreroItem.Activated += (sender, e) => SpawnVehicle("torero");
        sportsClassicSubMenu.Add(toreroItem);

        NativeItem tornadoItem = new NativeItem("Tornado");
        tornadoItem.Activated += (sender, e) => SpawnVehicle("tornado");
        sportsClassicSubMenu.Add(tornadoItem);

        NativeItem tornado2Item = new NativeItem("Tornado");
        tornado2Item.Activated += (sender, e) => SpawnVehicle("tornado2");
        sportsClassicSubMenu.Add(tornado2Item);

        NativeItem tornado3Item = new NativeItem("Tornado");
        tornado3Item.Activated += (sender, e) => SpawnVehicle("tornado3");
        sportsClassicSubMenu.Add(tornado3Item);

        NativeItem tornado4Item = new NativeItem("Tornado");
        tornado4Item.Activated += (sender, e) => SpawnVehicle("tornado4");
        sportsClassicSubMenu.Add(tornado4Item);

        NativeItem tornado5Item = new NativeItem("Tornado Custom");
        tornado5Item.Activated += (sender, e) => SpawnVehicle("tornado5");
        sportsClassicSubMenu.Add(tornado5Item);

        NativeItem tornado6Item = new NativeItem("Tornado Rat Rod");
        tornado6Item.Activated += (sender, e) => SpawnVehicle("tornado6");
        sportsClassicSubMenu.Add(tornado6Item);

        NativeItem turismoClassicItem = new NativeItem("Turismo Classic");
        turismoClassicItem.Activated += (sender, e) => SpawnVehicle("turismoclassic");
        sportsClassicSubMenu.Add(turismoClassicItem);

        NativeItem viserisItem = new NativeItem("Viseris");
        viserisItem.Activated += (sender, e) => SpawnVehicle("viseris");
        sportsClassicSubMenu.Add(viserisItem);

        NativeItem z190Item = new NativeItem("190z");
        z190Item.Activated += (sender, e) => SpawnVehicle("190z");
        sportsClassicSubMenu.Add(z190Item);

        NativeItem zionClassicItem = new NativeItem("Zion Classic");
        zionClassicItem.Activated += (sender, e) => SpawnVehicle("zionclassic");
        sportsClassicSubMenu.Add(zionClassicItem);

        NativeItem zTypeItem = new NativeItem("Z-Type");
        zTypeItem.Activated += (sender, e) => SpawnVehicle("ztype");
        sportsClassicSubMenu.Add(zTypeItem);

        // Super 

        NativeItem adderItem = new NativeItem("Adder");
        adderItem.Activated += (sender, e) => SpawnVehicle("adder");
        superSubMenu.Add(adderItem);

        NativeItem autarchItem = new NativeItem("Autarch");
        autarchItem.Activated += (sender, e) => SpawnVehicle("autarch");
        superSubMenu.Add(autarchItem);

        NativeItem banshee900RItem = new NativeItem("Banshee 900R");
        banshee900RItem.Activated += (sender, e) => SpawnVehicle("banshee900r");
        superSubMenu.Add(banshee900RItem);

        NativeItem bulletItem = new NativeItem("Bullet");
        bulletItem.Activated += (sender, e) => SpawnVehicle("bullet");
        superSubMenu.Add(bulletItem);

        NativeItem championItem = new NativeItem("Champion");
        championItem.Activated += (sender, e) => SpawnVehicle("champion");
        superSubMenu.Add(championItem);

        NativeItem cheetahItem = new NativeItem("Cheetah");
        cheetahItem.Activated += (sender, e) => SpawnVehicle("cheetah");
        superSubMenu.Add(cheetahItem);

        NativeItem cycloneItem = new NativeItem("Cyclone");
        cycloneItem.Activated += (sender, e) => SpawnVehicle("cyclone");
        superSubMenu.Add(cycloneItem);

        NativeItem devesteItem = new NativeItem("Deveste Eight");
        devesteItem.Activated += (sender, e) => SpawnVehicle("deveste");
        superSubMenu.Add(devesteItem);

        NativeItem emerusItem = new NativeItem("Emerus");
        emerusItem.Activated += (sender, e) => SpawnVehicle("emerus");
        superSubMenu.Add(emerusItem);

        NativeItem entityXXRItem = new NativeItem("Entity XXR");
        entityXXRItem.Activated += (sender, e) => SpawnVehicle("entity2");
        superSubMenu.Add(entityXXRItem);

        NativeItem entityMTItem = new NativeItem("Entity MT");
        entityMTItem.Activated += (sender, e) => SpawnVehicle("entity3");
        superSubMenu.Add(entityMTItem);

        NativeItem entityXFItem = new NativeItem("Entity XF");
        entityXFItem.Activated += (sender, e) => SpawnVehicle("entityxf");
        superSubMenu.Add(entityXFItem);

        NativeItem fmjItem = new NativeItem("FMJ");
        fmjItem.Activated += (sender, e) => SpawnVehicle("fmj");
        superSubMenu.Add(fmjItem);

        NativeItem furiaItem = new NativeItem("Furia");
        furiaItem.Activated += (sender, e) => SpawnVehicle("furia");
        superSubMenu.Add(furiaItem);

        NativeItem gp1Item = new NativeItem("GP1");
        gp1Item.Activated += (sender, e) => SpawnVehicle("gp1");
        superSubMenu.Add(gp1Item);

        NativeItem ignusItem = new NativeItem("Ignus");
        ignusItem.Activated += (sender, e) => SpawnVehicle("ignus");
        superSubMenu.Add(ignusItem);

        NativeItem infernusItem = new NativeItem("Infernus");
        infernusItem.Activated += (sender, e) => SpawnVehicle("infernus");
        superSubMenu.Add(infernusItem);

        NativeItem italiGTBItem = new NativeItem("Itali GTB");
        italiGTBItem.Activated += (sender, e) => SpawnVehicle("italigtb");
        superSubMenu.Add(italiGTBItem);

        NativeItem italiGTBCustomItem = new NativeItem("Itali GTB Custom");
        italiGTBCustomItem.Activated += (sender, e) => SpawnVehicle("italigtb2");
        superSubMenu.Add(italiGTBCustomItem);

        NativeItem kriegerItem = new NativeItem("Krieger");
        kriegerItem.Activated += (sender, e) => SpawnVehicle("krieger");
        superSubMenu.Add(kriegerItem);

        NativeItem le7bItem = new NativeItem("RE-7B");
        le7bItem.Activated += (sender, e) => SpawnVehicle("le7b");
        superSubMenu.Add(le7bItem);

        NativeItem lm87Item = new NativeItem("LM87");
        lm87Item.Activated += (sender, e) => SpawnVehicle("lm87");
        superSubMenu.Add(lm87Item);

        NativeItem neroItem = new NativeItem("Nero");
        neroItem.Activated += (sender, e) => SpawnVehicle("nero");
        superSubMenu.Add(neroItem);

        NativeItem neroCustomItem = new NativeItem("Nero Custom");
        neroCustomItem.Activated += (sender, e) => SpawnVehicle("nero2");
        superSubMenu.Add(neroCustomItem);

        NativeItem osirisItem = new NativeItem("Osiris");
        osirisItem.Activated += (sender, e) => SpawnVehicle("osiris");
        superSubMenu.Add(osirisItem);

        NativeItem penetratorItem = new NativeItem("Penetrator");
        penetratorItem.Activated += (sender, e) => SpawnVehicle("penetrator");
        superSubMenu.Add(penetratorItem);

        NativeItem pfister811Item = new NativeItem("811");
        pfister811Item.Activated += (sender, e) => SpawnVehicle("pfister811");
        superSubMenu.Add(pfister811Item);

        NativeItem prototipoItem = new NativeItem("X80 Proto");
        prototipoItem.Activated += (sender, e) => SpawnVehicle("prototipo");
        superSubMenu.Add(prototipoItem);

        NativeItem reaperItem = new NativeItem("Reaper");
        reaperItem.Activated += (sender, e) => SpawnVehicle("reaper");
        superSubMenu.Add(reaperItem);

        NativeItem s80RRItem = new NativeItem("S80RR");
        s80RRItem.Activated += (sender, e) => SpawnVehicle("s80");
        superSubMenu.Add(s80RRItem);

        NativeItem sc1Item = new NativeItem("SC1");
        sc1Item.Activated += (sender, e) => SpawnVehicle("sc1");
        superSubMenu.Add(sc1Item);

        NativeItem scramjetItem = new NativeItem("Scramjet");
        scramjetItem.Activated += (sender, e) => SpawnVehicle("scramjet");
        superSubMenu.Add(scramjetItem);

        NativeItem sheavaItem = new NativeItem("ETR1");
        sheavaItem.Activated += (sender, e) => SpawnVehicle("sheava");
        superSubMenu.Add(sheavaItem);

        NativeItem sultanRSItem = new NativeItem("Sultan RS");
        sultanRSItem.Activated += (sender, e) => SpawnVehicle("sultanrs");
        superSubMenu.Add(sultanRSItem);

        NativeItem t20Item = new NativeItem("T20");
        t20Item.Activated += (sender, e) => SpawnVehicle("t20");
        superSubMenu.Add(t20Item);

        NativeItem taipanItem = new NativeItem("Taipan");
        taipanItem.Activated += (sender, e) => SpawnVehicle("taipan");
        superSubMenu.Add(taipanItem);

        NativeItem tempestaItem = new NativeItem("Tempesta");
        tempestaItem.Activated += (sender, e) => SpawnVehicle("tempesta");
        superSubMenu.Add(tempestaItem);

        NativeItem tezeractItem = new NativeItem("Tezeract");
        tezeractItem.Activated += (sender, e) => SpawnVehicle("tezeract");
        superSubMenu.Add(tezeractItem);

        NativeItem thraxItem = new NativeItem("Thrax");
        thraxItem.Activated += (sender, e) => SpawnVehicle("thrax");
        superSubMenu.Add(thraxItem);

        NativeItem tigonItem = new NativeItem("Tigon");
        tigonItem.Activated += (sender, e) => SpawnVehicle("tigon");
        superSubMenu.Add(tigonItem);

        NativeItem toreroXOItem = new NativeItem("Torero XO");
        toreroXOItem.Activated += (sender, e) => SpawnVehicle("torero2");
        superSubMenu.Add(toreroXOItem);

        NativeItem turismoRItem = new NativeItem("Turismo R");
        turismoRItem.Activated += (sender, e) => SpawnVehicle("turismor");
        superSubMenu.Add(turismoRItem);

        NativeItem tyrantItem = new NativeItem("Tyrant");
        tyrantItem.Activated += (sender, e) => SpawnVehicle("tyrant");
        superSubMenu.Add(tyrantItem);

        NativeItem tyrusItem = new NativeItem("Tyrus");
        tyrusItem.Activated += (sender, e) => SpawnVehicle("tyrus");
        superSubMenu.Add(tyrusItem);

        NativeItem vaccaItem = new NativeItem("Vacca");
        vaccaItem.Activated += (sender, e) => SpawnVehicle("vacca");
        superSubMenu.Add(vaccaItem);

        NativeItem vagnerItem = new NativeItem("Vagner");
        vagnerItem.Activated += (sender, e) => SpawnVehicle("vagner");
        superSubMenu.Add(vagnerItem);

        NativeItem vigilanteItem = new NativeItem("Vigilante");
        vigilanteItem.Activated += (sender, e) => SpawnVehicle("vigilante");
        superSubMenu.Add(vigilanteItem);

        NativeItem virtueItem = new NativeItem("Virtue");
        virtueItem.Activated += (sender, e) => SpawnVehicle("virtue");
        superSubMenu.Add(virtueItem);

        NativeItem visioneItem = new NativeItem("Visione");
        visioneItem.Activated += (sender, e) => SpawnVehicle("visione");
        superSubMenu.Add(visioneItem);

        NativeItem volticItem = new NativeItem("Voltic");
        volticItem.Activated += (sender, e) => SpawnVehicle("voltic");
        superSubMenu.Add(volticItem);

        NativeItem rocketVolticItem = new NativeItem("Rocket Voltic");
        rocketVolticItem.Activated += (sender, e) => SpawnVehicle("voltic2");
        superSubMenu.Add(rocketVolticItem);

        NativeItem xa21Item = new NativeItem("XA-21");
        xa21Item.Activated += (sender, e) => SpawnVehicle("xa21");
        superSubMenu.Add(xa21Item);

        NativeItem zenoItem = new NativeItem("Zeno");
        zenoItem.Activated += (sender, e) => SpawnVehicle("zeno");
        superSubMenu.Add(zenoItem);

        NativeItem zentornoItem = new NativeItem("Zentorno");
        zentornoItem.Activated += (sender, e) => SpawnVehicle("zentorno");
        superSubMenu.Add(zentornoItem);

        NativeItem zorrussoItem = new NativeItem("Zorrusso");
        zorrussoItem.Activated += (sender, e) => SpawnVehicle("zorrusso");
        superSubMenu.Add(zorrussoItem);

        // Suv

        NativeItem astronItem = new NativeItem("Astron");
        astronItem.Activated += (sender, e) => SpawnVehicle("astron");
        suvSubMenu.Add(astronItem);

        NativeItem ballerItem = new NativeItem("Baller");
        ballerItem.Activated += (sender, e) => SpawnVehicle("baller");
        suvSubMenu.Add(ballerItem);

        NativeItem baller2Item = new NativeItem("Baller");
        baller2Item.Activated += (sender, e) => SpawnVehicle("baller2");
        suvSubMenu.Add(baller2Item);

        NativeItem baller3Item = new NativeItem("Baller LE");
        baller3Item.Activated += (sender, e) => SpawnVehicle("baller3");
        suvSubMenu.Add(baller3Item);

        NativeItem baller4Item = new NativeItem("Baller LE LWB");
        baller4Item.Activated += (sender, e) => SpawnVehicle("baller4");
        suvSubMenu.Add(baller4Item);

        NativeItem baller5Item = new NativeItem("Baller LE (Armored)");
        baller5Item.Activated += (sender, e) => SpawnVehicle("baller5");
        suvSubMenu.Add(baller5Item);

        NativeItem baller6Item = new NativeItem("Baller LE LWB (Armored)");
        baller6Item.Activated += (sender, e) => SpawnVehicle("baller6");
        suvSubMenu.Add(baller6Item);

        NativeItem baller7Item = new NativeItem("Baller ST");
        baller7Item.Activated += (sender, e) => SpawnVehicle("baller7");
        suvSubMenu.Add(baller7Item);

        NativeItem bjXLItem = new NativeItem("BjXL");
        bjXLItem.Activated += (sender, e) => SpawnVehicle("bjxl");
        suvSubMenu.Add(bjXLItem);

        NativeItem cavalcadeItem = new NativeItem("Cavalcade");
        cavalcadeItem.Activated += (sender, e) => SpawnVehicle("cavalcade");
        suvSubMenu.Add(cavalcadeItem);

        NativeItem cavalcade2Item = new NativeItem("Cavalcade");
        cavalcade2Item.Activated += (sender, e) => SpawnVehicle("cavalcade2");
        suvSubMenu.Add(cavalcade2Item);

        NativeItem contenderItem = new NativeItem("Contender");
        contenderItem.Activated += (sender, e) => SpawnVehicle("contender");
        suvSubMenu.Add(contenderItem);

        NativeItem dubstaItem = new NativeItem("Dubsta");
        dubstaItem.Activated += (sender, e) => SpawnVehicle("dubsta");
        suvSubMenu.Add(dubstaItem);

        NativeItem dubsta2Item = new NativeItem("Dubsta");
        dubsta2Item.Activated += (sender, e) => SpawnVehicle("dubsta2");
        suvSubMenu.Add(dubsta2Item);

        NativeItem fq2Item = new NativeItem("FQ 2");
        fq2Item.Activated += (sender, e) => SpawnVehicle("fq2");
        suvSubMenu.Add(fq2Item);

        NativeItem grangerItem = new NativeItem("Granger");
        grangerItem.Activated += (sender, e) => SpawnVehicle("granger");
        suvSubMenu.Add(grangerItem);

        NativeItem granger2Item = new NativeItem("Granger 3600LX");
        granger2Item.Activated += (sender, e) => SpawnVehicle("granger2");
        suvSubMenu.Add(granger2Item);

        NativeItem gresleyItem = new NativeItem("Gresley");
        gresleyItem.Activated += (sender, e) => SpawnVehicle("gresley");
        suvSubMenu.Add(gresleyItem);

        NativeItem habaneroItem = new NativeItem("Habanero");
        habaneroItem.Activated += (sender, e) => SpawnVehicle("habanero");
        suvSubMenu.Add(habaneroItem);

        NativeItem huntleyItem = new NativeItem("Huntley S");
        huntleyItem.Activated += (sender, e) => SpawnVehicle("huntley");
        suvSubMenu.Add(huntleyItem);

        NativeItem issi8Item = new NativeItem("Issi Rally");
        issi8Item.Activated += (sender, e) => SpawnVehicle("issi8");
        suvSubMenu.Add(issi8Item);

        NativeItem iwagenItem = new NativeItem("I-Wagen");
        iwagenItem.Activated += (sender, e) => SpawnVehicle("iwagen");
        suvSubMenu.Add(iwagenItem);

        NativeItem jubileeItem = new NativeItem("Jubilee");
        jubileeItem.Activated += (sender, e) => SpawnVehicle("jubilee");
        suvSubMenu.Add(jubileeItem);

        NativeItem landstalkerItem = new NativeItem("Landstalker");
        landstalkerItem.Activated += (sender, e) => SpawnVehicle("landstalker");
        suvSubMenu.Add(landstalkerItem);

        NativeItem landstalker2Item = new NativeItem("Landstalker XL");
        landstalker2Item.Activated += (sender, e) => SpawnVehicle("landstalker2");
        suvSubMenu.Add(landstalker2Item);

        NativeItem mesaItem = new NativeItem("Mesa");
        mesaItem.Activated += (sender, e) => SpawnVehicle("mesa");
        suvSubMenu.Add(mesaItem);

        NativeItem mesa2Item = new NativeItem("Mesa");
        mesa2Item.Activated += (sender, e) => SpawnVehicle("mesa2");
        suvSubMenu.Add(mesa2Item);

        NativeItem novakItem = new NativeItem("Novak");
        novakItem.Activated += (sender, e) => SpawnVehicle("novak");
        suvSubMenu.Add(novakItem);

        NativeItem patriotItem = new NativeItem("Patriot");
        patriotItem.Activated += (sender, e) => SpawnVehicle("patriot");
        suvSubMenu.Add(patriotItem);

        NativeItem patriot2Item = new NativeItem("Patriot Stretch");
        patriot2Item.Activated += (sender, e) => SpawnVehicle("patriot2");
        suvSubMenu.Add(patriot2Item);

        NativeItem radiItem = new NativeItem("Radius");
        radiItem.Activated += (sender, e) => SpawnVehicle("radi");
        suvSubMenu.Add(radiItem);

        NativeItem reblaItem = new NativeItem("Rebla GTS");
        reblaItem.Activated += (sender, e) => SpawnVehicle("rebla");
        suvSubMenu.Add(reblaItem);

        NativeItem rocotoItem = new NativeItem("Rocoto");
        rocotoItem.Activated += (sender, e) => SpawnVehicle("rocoto");
        suvSubMenu.Add(rocotoItem);

        NativeItem seminoleItem = new NativeItem("Seminole");
        seminoleItem.Activated += (sender, e) => SpawnVehicle("seminole");
        suvSubMenu.Add(seminoleItem);

        NativeItem seminole2Item = new NativeItem("Seminole Frontier");
        seminole2Item.Activated += (sender, e) => SpawnVehicle("seminole2");
        suvSubMenu.Add(seminole2Item);

        NativeItem serranoItem = new NativeItem("Serrano");
        serranoItem.Activated += (sender, e) => SpawnVehicle("serrano");
        suvSubMenu.Add(serranoItem);

        NativeItem squaddieItem = new NativeItem("Squaddie");
        squaddieItem.Activated += (sender, e) => SpawnVehicle("squaddie");
        suvSubMenu.Add(squaddieItem);

        NativeItem torosItem = new NativeItem("Toros");
        torosItem.Activated += (sender, e) => SpawnVehicle("toros");
        suvSubMenu.Add(torosItem);

        NativeItem xlsItem = new NativeItem("XLS");
        xlsItem.Activated += (sender, e) => SpawnVehicle("xls");
        suvSubMenu.Add(xlsItem);

        NativeItem xls2Item = new NativeItem("XLS (Armored)");
        xls2Item.Activated += (sender, e) => SpawnVehicle("xls2");
        suvSubMenu.Add(xls2Item);

        // Utility

        NativeItem airtugItem = new NativeItem("Airtug");
        airtugItem.Activated += (sender, e) => SpawnVehicle("airtug");
        utilitySubMenu.Add(airtugItem);

        NativeItem armyTrailerItem = new NativeItem("Army Trailer");
        armyTrailerItem.Activated += (sender, e) => SpawnVehicle("armytrailer");
        utilitySubMenu.Add(armyTrailerItem);

        NativeItem baletrailerItem = new NativeItem("Baletrailer");
        baletrailerItem.Activated += (sender, e) => SpawnVehicle("baletrailer");
        utilitySubMenu.Add(baletrailerItem);

        NativeItem boatTrailerItem = new NativeItem("Boat Trailer");
        boatTrailerItem.Activated += (sender, e) => SpawnVehicle("boattrailer");
        utilitySubMenu.Add(boatTrailerItem);

        NativeItem caddyItem = new NativeItem("Caddy");
        caddyItem.Activated += (sender, e) => SpawnVehicle("caddy");
        utilitySubMenu.Add(caddyItem);

        NativeItem docktugItem = new NativeItem("Docktug");
        docktugItem.Activated += (sender, e) => SpawnVehicle("docktug");
        utilitySubMenu.Add(docktugItem);

        NativeItem forkliftItem = new NativeItem("Forklift");
        forkliftItem.Activated += (sender, e) => SpawnVehicle("forklift");
        utilitySubMenu.Add(forkliftItem);

        NativeItem graintrailerItem = new NativeItem("Graintrailer");
        graintrailerItem.Activated += (sender, e) => SpawnVehicle("graintrailer");
        utilitySubMenu.Add(graintrailerItem);

        NativeItem lawnMowerItem = new NativeItem("Lawn Mower");
        lawnMowerItem.Activated += (sender, e) => SpawnVehicle("mower");
        utilitySubMenu.Add(lawnMowerItem);

        NativeItem ripleyItem = new NativeItem("Ripley");
        ripleyItem.Activated += (sender, e) => SpawnVehicle("ripley");
        utilitySubMenu.Add(ripleyItem);

        NativeItem sadlerItem = new NativeItem("Sadler");
        sadlerItem.Activated += (sender, e) => SpawnVehicle("sadler");
        utilitySubMenu.Add(sadlerItem);

        NativeItem scrapTruckItem = new NativeItem("Scrap Truck");
        scrapTruckItem.Activated += (sender, e) => SpawnVehicle("scrap");
        utilitySubMenu.Add(scrapTruckItem);

        NativeItem slamtruckItem = new NativeItem("Slamtruck");
        slamtruckItem.Activated += (sender, e) => SpawnVehicle("slamtruck");
        utilitySubMenu.Add(slamtruckItem);

        NativeItem trailerItem = new NativeItem("Trailer");
        trailerItem.Activated += (sender, e) => SpawnVehicle("trailer");
        utilitySubMenu.Add(trailerItem);

        NativeItem towtruckItem = new NativeItem("Towtruck");
        towtruckItem.Activated += (sender, e) => SpawnVehicle("towtruck");
        utilitySubMenu.Add(towtruckItem);

        NativeItem tractorItem = new NativeItem("Tractor");
        tractorItem.Activated += (sender, e) => SpawnVehicle("tractor");
        utilitySubMenu.Add(tractorItem);

        NativeItem fieldmasterItem = new NativeItem("Fieldmaster");
        fieldmasterItem.Activated += (sender, e) => SpawnVehicle("tractor2");
        utilitySubMenu.Add(fieldmasterItem);

        NativeItem mocItem = new NativeItem("Mobile Operations Center");
        mocItem.Activated += (sender, e) => SpawnVehicle("trailerlarge");
        utilitySubMenu.Add(mocItem);

        NativeItem trailerlogsItem = new NativeItem("Trailer Logs");
        trailerlogsItem.Activated += (sender, e) => SpawnVehicle("trailerlogs");
        utilitySubMenu.Add(trailerlogsItem);

        NativeItem trailersItem = new NativeItem("Trailers");
        trailersItem.Activated += (sender, e) => SpawnVehicle("trailers");
        utilitySubMenu.Add(trailersItem);

        NativeItem trailerslogsItem = new NativeItem("Trailer Logs");
        trailerslogsItem.Activated += (sender, e) => SpawnVehicle("trailerlogs");
        utilitySubMenu.Add(trailerslogsItem);

        NativeItem trailersmallItem = new NativeItem("Trailer Small");
        trailersmallItem.Activated += (sender, e) => SpawnVehicle("trailersmall");
        utilitySubMenu.Add(trailersmallItem);

        NativeItem trflatItem = new NativeItem("Trailer Flat");
        trflatItem.Activated += (sender, e) => SpawnVehicle("trflat");
        utilitySubMenu.Add(trflatItem);

        NativeItem tvtrailerItem = new NativeItem("TV Trailer");
        tvtrailerItem.Activated += (sender, e) => SpawnVehicle("tvtrailer");
        utilitySubMenu.Add(tvtrailerItem);

        NativeItem utillitruckItem = new NativeItem("Utility Truck 1");
        utillitruckItem.Activated += (sender, e) => SpawnVehicle("utillitruck");
        utilitySubMenu.Add(utillitruckItem);

        NativeItem utillitruck2Item = new NativeItem("Utility Truck 2");
        utillitruck2Item.Activated += (sender, e) => SpawnVehicle("utillitruck2");
        utilitySubMenu.Add(utillitruck2Item);

        NativeItem utillitruck3Item = new NativeItem("Utility Truck 3");
        utillitruck3Item.Activated += (sender, e) => SpawnVehicle("utillitruck3");
        utilitySubMenu.Add(utillitruck3Item);



        // Vans

        NativeItem bison1Item = new NativeItem("Bison 1");
        bison1Item.Activated += (sender, e) => SpawnVehicle("bison1");
        vanSubMenu.Add(bison1Item);

        NativeItem bison2Item = new NativeItem("Bison 2");
        bison2Item.Activated += (sender, e) => SpawnVehicle("bison2");
        vanSubMenu.Add(bison2Item);

        NativeItem bison3Item = new NativeItem("Bison 3");
        bison3Item.Activated += (sender, e) => SpawnVehicle("bison3");
        vanSubMenu.Add(bison3Item);

        NativeItem bobcatXLItem = new NativeItem("Bobcat XL 1");
        bobcatXLItem.Activated += (sender, e) => SpawnVehicle("bobcatxl");
        vanSubMenu.Add(bobcatXLItem);

        NativeItem boxville1Item = new NativeItem("Boxville 1");
        boxville1Item.Activated += (sender, e) => SpawnVehicle("boxville1");
        vanSubMenu.Add(boxville1Item);

        NativeItem boxville2Item = new NativeItem("Boxville 2");
        boxville2Item.Activated += (sender, e) => SpawnVehicle("boxville2");
        vanSubMenu.Add(boxville2Item);

        NativeItem boxville3Item = new NativeItem("Boxville 3");
        boxville3Item.Activated += (sender, e) => SpawnVehicle("boxville3");
        vanSubMenu.Add(boxville3Item);

        NativeItem boxville4Item = new NativeItem("Boxville 4");
        boxville4Item.Activated += (sender, e) => SpawnVehicle("boxville4");
        vanSubMenu.Add(boxville4Item);

        NativeItem boxville5Item = new NativeItem("Boxville 5");
        boxville5Item.Activated += (sender, e) => SpawnVehicle("boxville5");
        vanSubMenu.Add(boxville5Item);

        NativeItem boxville6Item = new NativeItem("Boxville 6");
        boxville6Item.Activated += (sender, e) => SpawnVehicle("boxville6");
        vanSubMenu.Add(boxville6Item);

        NativeItem burrito1Item = new NativeItem("Burrito 1");
        burrito1Item.Activated += (sender, e) => SpawnVehicle("burrito1");
        vanSubMenu.Add(burrito1Item);

        NativeItem burrito2Item = new NativeItem("Burrito 2");
        burrito2Item.Activated += (sender, e) => SpawnVehicle("burrito2");
        vanSubMenu.Add(burrito2Item);

        NativeItem burrito3Item = new NativeItem("Burrito 3");
        burrito3Item.Activated += (sender, e) => SpawnVehicle("burrito3");
        vanSubMenu.Add(burrito3Item);

        NativeItem burrito4Item = new NativeItem("Burrito 4");
        burrito4Item.Activated += (sender, e) => SpawnVehicle("burrito4");
        vanSubMenu.Add(burrito4Item);

        NativeItem burrito5Item = new NativeItem("Burrito 5");
        burrito5Item.Activated += (sender, e) => SpawnVehicle("burrito5");
        vanSubMenu.Add(burrito5Item);

        NativeItem camper1Item = new NativeItem("Camper 1");
        camper1Item.Activated += (sender, e) => SpawnVehicle("camper1");
        vanSubMenu.Add(camper1Item);

        NativeItem gburrito1Item = new NativeItem("Gang Burrito 1");
        gburrito1Item.Activated += (sender, e) => SpawnVehicle("gburrito1");
        vanSubMenu.Add(gburrito1Item);

        NativeItem gburrito2Item = new NativeItem("Gang Burrito 2");
        gburrito2Item.Activated += (sender, e) => SpawnVehicle("gburrito2");
        vanSubMenu.Add(gburrito2Item);

        NativeItem journey1Item = new NativeItem("Journey 1");
        journey1Item.Activated += (sender, e) => SpawnVehicle("journey1");
        vanSubMenu.Add(journey1Item);

        NativeItem journey2Item = new NativeItem("Journey II 1");
        journey2Item.Activated += (sender, e) => SpawnVehicle("journey2");
        vanSubMenu.Add(journey2Item);

        NativeItem minivan1Item = new NativeItem("Minivan 1");
        minivan1Item.Activated += (sender, e) => SpawnVehicle("minivan1");
        vanSubMenu.Add(minivan1Item);

        NativeItem minivan2Item = new NativeItem("Minivan Custom 1");
        minivan2Item.Activated += (sender, e) => SpawnVehicle("minivan2");
        vanSubMenu.Add(minivan2Item);

        NativeItem paradise1Item = new NativeItem("Paradise 1");
        paradise1Item.Activated += (sender, e) => SpawnVehicle("paradise1");
        vanSubMenu.Add(paradise1Item);

        NativeItem pony1Item = new NativeItem("Pony 1");
        pony1Item.Activated += (sender, e) => SpawnVehicle("pony1");
        vanSubMenu.Add(pony1Item);

        NativeItem pony2Item = new NativeItem("Pony 2");
        pony2Item.Activated += (sender, e) => SpawnVehicle("pony2");
        vanSubMenu.Add(pony2Item);

        NativeItem rumpo1Item = new NativeItem("Rumpo 1");
        rumpo1Item.Activated += (sender, e) => SpawnVehicle("rumpo1");
        vanSubMenu.Add(rumpo1Item);

        NativeItem rumpo2Item = new NativeItem("Rumpo 2");
        rumpo2Item.Activated += (sender, e) => SpawnVehicle("rumpo2");
        vanSubMenu.Add(rumpo2Item);

        NativeItem rumpo3Item = new NativeItem("Rumpo Custom 1");
        rumpo3Item.Activated += (sender, e) => SpawnVehicle("rumpo3");
        vanSubMenu.Add(rumpo3Item);

        NativeItem speedo1Item = new NativeItem("Speedo 1");
        speedo1Item.Activated += (sender, e) => SpawnVehicle("speedo1");
        vanSubMenu.Add(speedo1Item);

        NativeItem speedo2Item = new NativeItem("Clown Van 1");
        speedo2Item.Activated += (sender, e) => SpawnVehicle("speedo2");
        vanSubMenu.Add(speedo2Item);

        NativeItem speedo4Item = new NativeItem("Speedo Custom 1");
        speedo4Item.Activated += (sender, e) => SpawnVehicle("speedo4");
        vanSubMenu.Add(speedo4Item);

        NativeItem surfer1Item = new NativeItem("Surfer 1");
        surfer1Item.Activated += (sender, e) => SpawnVehicle("surfer1");
        vanSubMenu.Add(surfer1Item);

        NativeItem surfer2Item = new NativeItem("Surfer 2");
        surfer2Item.Activated += (sender, e) => SpawnVehicle("surfer2");
        vanSubMenu.Add(surfer2Item);

        NativeItem surfer3Item = new NativeItem("Surfer Custom 1");
        surfer3Item.Activated += (sender, e) => SpawnVehicle("surfer3");
        vanSubMenu.Add(surfer3Item);

        NativeItem taco1Item = new NativeItem("Taco Van 1");
        taco1Item.Activated += (sender, e) => SpawnVehicle("taco1");
        vanSubMenu.Add(taco1Item);

        NativeItem youga1Item = new NativeItem("Youga 1");
        youga1Item.Activated += (sender, e) => SpawnVehicle("youga1");
        vanSubMenu.Add(youga1Item);

        NativeItem youga2Item = new NativeItem("Youga Classic 1");
        youga2Item.Activated += (sender, e) => SpawnVehicle("youga2");
        vanSubMenu.Add(youga2Item);

        NativeItem youga3Item = new NativeItem("Youga Classic 4x4 1");
        youga3Item.Activated += (sender, e) => SpawnVehicle("youga3");
        vanSubMenu.Add(youga3Item);

        NativeItem youga4Item = new NativeItem("Youga Custom 1");
        youga4Item.Activated += (sender, e) => SpawnVehicle("youga4");
        vanSubMenu.Add(youga4Item);


        // Others

        NativeItem arbiterGTItem = new NativeItem("Arbiter GT");
        arbiterGTItem.Activated += (sender, e) => SpawnVehicle("arbitergt");
        othersSubMenu.Add(arbiterGTItem);

        NativeItem astron2Item = new NativeItem("Astron Custom");
        astron2Item.Activated += (sender, e) => SpawnVehicle("astron2");
        othersSubMenu.Add(astron2Item);

        NativeItem cyclone2Item = new NativeItem("Cyclone II");
        cyclone2Item.Activated += (sender, e) => SpawnVehicle("cyclone2");
        othersSubMenu.Add(cyclone2Item);

        NativeItem ignus2Item = new NativeItem("Weaponized Ignus");
        ignus2Item.Activated += (sender, e) => SpawnVehicle("ignus2");
        othersSubMenu.Add(ignus2Item);

        NativeItem s95Item = new NativeItem("S95");
        s95Item.Activated += (sender, e) => SpawnVehicle("s95");
        othersSubMenu.Add(s95Item);



















        // Teleport 

       

        // Recovery

       






        // Money

        // Add

        AddRemoveMoneyMenuItem(addSubMenu, "200K", 200000);
        AddRemoveMoneyMenuItem(addSubMenu, "500K", 500000);
        AddRemoveMoneyMenuItem(addSubMenu, "600K", 600000);
        AddRemoveMoneyMenuItem(addSubMenu, "900K", 900000);
        AddRemoveMoneyMenuItem(addSubMenu, "2 Million", 2000000);
        AddRemoveMoneyMenuItem(addSubMenu, "5 Million", 5000000);
        AddRemoveMoneyMenuItem(addSubMenu, "9 Million", 9000000);
        AddRemoveMoneyMenuItem(addSubMenu, "200 Million", 200000000);
        AddRemoveMoneyMenuItem(addSubMenu, "500 Million", 500000000);
        AddRemoveMoneyMenuItem(addSubMenu, "600 Million", 600000000);
        AddRemoveMoneyMenuItem(addSubMenu, "900 Million", 900000000);
        AddRemoveMoneyMenuItem(addSubMenu, "1 Billion", 1000000000);
        AddRemoveMoneyMenuItem(addSubMenu, "2 Billion", 2000000000);
        AddRemoveMoneyMenuItem(addSubMenu, "Max Bank", 2147483647);

        // Remove options

        AddRemoveMoneyMenuItem(removeSubMenu, "200K", -200000);
        AddRemoveMoneyMenuItem(removeSubMenu, "500K", -500000);
        AddRemoveMoneyMenuItem(removeSubMenu, "600K", -600000);
        AddRemoveMoneyMenuItem(removeSubMenu, "900K", -900000);
        AddRemoveMoneyMenuItem(removeSubMenu, "2 Million", -2000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "5 Million", -5000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "9 Million", -9000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "200 Million", -200000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "500 Million", -500000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "600 Million", -600000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "900 Million", -900000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "1 Billion", -1000000000);
        AddRemoveMoneyMenuItem(removeSubMenu, "2 Billion", -2000000000);
        AddClearBankMenuItem(removeSubMenu, "Clear Bank");

        // World





        // Time

        NativeItem sunriseItem = new NativeItem("Sunrise");
        sunriseItem.Activated += (sender, e) => SetTime(6, 0);
        timeSubMenu.Add(sunriseItem);

        NativeItem morningItem = new NativeItem("Morning");
        morningItem.Activated += (sender, e) => SetTime(9, 0);
        timeSubMenu.Add(morningItem);

        NativeItem noonItem = new NativeItem("Noon");
        noonItem.Activated += (sender, e) => SetTime(12, 0);
        timeSubMenu.Add(noonItem);

        NativeItem afternoonItem = new NativeItem("Afternoon");
        afternoonItem.Activated += (sender, e) => SetTime(15, 0);
        timeSubMenu.Add(afternoonItem);

        NativeItem sunsetItem = new NativeItem("Sunset");
        sunsetItem.Activated += (sender, e) => SetTime(18, 0);
        timeSubMenu.Add(sunsetItem);

        NativeItem nightItem = new NativeItem("Night");
        nightItem.Activated += (sender, e) => SetTime(21, 0);
        timeSubMenu.Add(nightItem);

        NativeItem midnightItem = new NativeItem("Midnight");
        midnightItem.Activated += (sender, e) => SetTime(0, 0);
        timeSubMenu.Add(midnightItem);


        // Weather 



        // Weather Hashes Defined

        const uint ClearHash = 0x36A83D84;
        const uint ExtrasunnyHash = 0x97AA0A79;
        const uint CloudsHash = 0x30FDAF5C;
        const uint OvercastHash = 0xBB898D2D;
        const uint RainHash = 0x54A69840;
        const uint ClearingHash = 0x6DB1A50D;
        const uint ThunderHash = 0xB677829F;
        const uint SmogHash = 0x10DCF4B5;
        const uint FoggyHash = 0xAE737644;
        const uint XmasHash = 0xAAC9C895;
        const uint SnowlightHash = 0x23FB812B;
        const uint BlizzardHash = 0x27EA2814;

        // Add weather menu items
        AddWeatherMenuItem("CLEAR", "Clear", "Set weather to clear.", ClearHash);
        AddWeatherMenuItem("EXTRASUNNY", "Extrasunny", "Set weather to extrasunny.", ExtrasunnyHash);
        AddWeatherMenuItem("CLOUDS", "Clouds", "Set weather to clouds.", CloudsHash);
        AddWeatherMenuItem("OVERCAST", "Overcast", "Set weather to overcast.", OvercastHash);
        AddWeatherMenuItem("RAIN", "Rain", "Set weather to rain.", RainHash);
        AddWeatherMenuItem("CLEARING", "Clearing", "Set weather to clearing.", ClearingHash);
        AddWeatherMenuItem("THUNDER", "Thunder", "Set weather to thunder.", ThunderHash);
        AddWeatherMenuItem("SMOG", "Smog", "Set weather to smog.", SmogHash);
        AddWeatherMenuItem("FOGGY", "Foggy", "Set weather to foggy.", FoggyHash);
        AddWeatherMenuItem("XMAS", "Xmas", "Set weather to Xmas.", XmasHash);
        AddWeatherMenuItem("SNOWLIGHT", "Snowlight", "Set weather to snowlight.", SnowlightHash);
        AddWeatherMenuItem("BLIZZARD", "Blizzard", "Set weather to blizzard.", BlizzardHash);


        // Misc

        NativeItem closeGtaItem = new NativeItem("Close GTA", "This Will ~r~Terminate~s~ The GTA Process");
        closeGtaItem.Activated += (sender, e) => CloseGta();
        miscSubMenu.Add(closeGtaItem);


  


        // Settings 

        NativeCheckboxItem disableMouseControlsCheckbox = new NativeCheckboxItem("Disable Visible Mouse", "Disables the mouse on screen. (Mouse controls are still active)", false);
        disableMouseControlsCheckbox.CheckboxChanged += (sender, e) => DisableMouseControlsToggle_CheckboxChanged(sender, e);
        disableSubMenu.Add(disableMouseControlsCheckbox);

        NativeCheckboxItem removeHelpTextCheckbox = new NativeCheckboxItem("Remove Help Text");
        removeHelpTextCheckbox.CheckboxChanged += (sender, e) => RemoveHelpTextToggle_CheckboxChanged(sender, e);
        disableSubMenu.Add(removeHelpTextCheckbox);

      




        // UI Drawing

        mainMenu.Add(localSubMenu);
        mainMenu.Add(weaponsSubMenu);
        mainMenu.Add(vehiclesSubMenu);
        mainMenu.Add(spawnerSubMenu);
        spawnerSubMenu.Add(spawn_vehicleSubMenu);
        spawn_vehicleSubMenu.Add(boatsSubMenu);
        spawn_vehicleSubMenu.Add(commercialSubMenu);
        spawn_vehicleSubMenu.Add(compactSubMenu);
        spawn_vehicleSubMenu.Add(coupeSubMenu);
        spawn_vehicleSubMenu.Add(cycleSubMenu);
        spawn_vehicleSubMenu.Add(emergencySubMenu);
        spawn_vehicleSubMenu.Add(helicopterSubMenu);
        spawn_vehicleSubMenu.Add(industrialSubMenu);
        spawn_vehicleSubMenu.Add(militarySubMenu);
        spawn_vehicleSubMenu.Add(motorcycleSubMenu);
        spawn_vehicleSubMenu.Add(muscleSubMenu);
        spawn_vehicleSubMenu.Add(offRoadSubMenu);
        spawn_vehicleSubMenu.Add(openWheelSubMenu);
        spawn_vehicleSubMenu.Add(planeSubMenu);
        spawn_vehicleSubMenu.Add(railSubMenu);
        spawn_vehicleSubMenu.Add(sedanSubMenu);
        spawn_vehicleSubMenu.Add(serviceSubMenu);
        spawn_vehicleSubMenu.Add(sportsSubMenu);
        spawn_vehicleSubMenu.Add(sportsClassicSubMenu);
        spawn_vehicleSubMenu.Add(superSubMenu);
        spawn_vehicleSubMenu.Add(suvSubMenu);
        spawn_vehicleSubMenu.Add(utilitySubMenu);
        spawn_vehicleSubMenu.Add(vanSubMenu);
        spawn_vehicleSubMenu.Add(othersSubMenu);
        mainMenu.Add(teleportSubMenu);
        mainMenu.Add(recoverySubMenu);
        recoverySubMenu.Add(moneySubMenu);
        moneySubMenu.Add(addSubMenu);
        moneySubMenu.Add(removeSubMenu);
        mainMenu.Add(worldSubMenu);
        worldSubMenu.Add(timeSubMenu);
        worldSubMenu.Add(weatherSubMenu);
        mainMenu.Add(miscSubMenu);
        mainMenu.Add(settingsSubMenu);
        settingsSubMenu.Add(disableSubMenu);


        pool.Add(mainMenu);
        pool.Add(localSubMenu);
        pool.Add(weaponsSubMenu);
        pool.Add(vehiclesSubMenu);
        pool.Add(spawnerSubMenu);
        pool.Add(spawn_vehicleSubMenu);
        pool.Add(boatsSubMenu);
        pool.Add(commercialSubMenu);
        pool.Add(compactSubMenu);
        pool.Add(coupeSubMenu);
        pool.Add(cycleSubMenu);
        pool.Add(emergencySubMenu);
        pool.Add(helicopterSubMenu);
        pool.Add(industrialSubMenu);
        pool.Add(militarySubMenu);
        pool.Add(motorcycleSubMenu);
        pool.Add(muscleSubMenu);
        pool.Add(offRoadSubMenu);
        pool.Add(openWheelSubMenu);
        pool.Add(planeSubMenu);
        pool.Add(railSubMenu);
        pool.Add(sedanSubMenu);
        pool.Add(serviceSubMenu);
        pool.Add(sportsSubMenu);
        pool.Add(sportsClassicSubMenu);
        pool.Add(superSubMenu);
        pool.Add(suvSubMenu);
        pool.Add(utilitySubMenu);
        pool.Add(vanSubMenu);
        pool.Add(othersSubMenu);
        pool.Add(teleportSubMenu);
        pool.Add(recoverySubMenu);
        pool.Add(moneySubMenu);
        pool.Add(addSubMenu);
        pool.Add(removeSubMenu);
        pool.Add(worldSubMenu);
        pool.Add(timeSubMenu);
        pool.Add(weatherSubMenu);
        pool.Add(miscSubMenu);
        pool.Add(settingsSubMenu);
        pool.Add(disableSubMenu);


        Tick += OnTick;
        KeyUp += OnKeyUp;
    }

    public class IniFile
    {
        private readonly string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public IniFile(string iniPath)
        {
            path = iniPath;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        public string Read(string section, string key, string defaultValue = "")
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, defaultValue, temp, 255, path);
            return temp.ToString();
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        pool.Process();

        God();
        Lawless();

      

        if (infiniteSpecialAbilityEnabled)
        {
            SetInfiniteSpecialAbility();
        }

        // Get the GTA V directory
        string gtaVDirectory = GetGtaVDirectory();

        // Create folders within the "scripts" folder
        CreateFolder(GetFolderPath(gtaVDirectory, "scripts", "RageTrainer"));
        CreateFolder(GetFolderPath(gtaVDirectory, "scripts", "RageTrainer\\Logging"));
        CreateFolder(GetFolderPath(gtaVDirectory, "scripts", "RageTrainer\\Settings"));
    }

    private void CreateFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private string GetFolderPath(string baseFolder, params string[] subfolders)
    {
        List<string> pathSegments = new List<string> { baseFolder };
        pathSegments.AddRange(subfolders);
        return Path.Combine(pathSegments.ToArray());
    }

    private string GetGtaVDirectory()
    {
        string[] registryKeys = {
        "SOFTWARE\\WOW6432Node\\Rockstar Games\\Epic Games Launcher\\Installed Games\\Grand Theft Auto V",
        "SOFTWARE\\WOW6432Node\\Rockstar Games\\Grand Theft Auto V",
        "SOFTWARE\\WOW6432Node\\Valve\\Steam\\Apps\\271590"
    };

        foreach (var keyPath in registryKeys)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
                {
                    if (key != null)
                    {
                        return key.GetValue("InstallFolder")?.ToString();
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if necessary
            }
        }

        // If registry checks fail, use a default path
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam", "steamapps", "common", "Grand Theft Auto V");
    }










    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F3)
        {
            ToggleMenu();
        }
    }



    private void ToggleMenu()
    {
        if (pool.AreAnyVisible)
        {
            pool.HideAll();
        }
        else
        {
            mainMenu.Visible = true;
        }
    }

   

    // Local

    private void God()
    {
        if (godModeEnabled)
        {
            Game.Player.Character.IsInvincible = true;
        }
        else
        {
            Game.Player.Character.IsInvincible = false;
        }
    }

    private bool GodToggle
    {
        get { return godModeEnabled; }
        set { godModeEnabled = value; }
    }

    private void Lawless()
    {

    }

    private bool LawlessToggle
    {
        get { return lawlessEnabled; }
        set
        {
            lawlessEnabled = value;

            if (lawlessEnabled)
            {
                Game.Player.WantedLevel = 0;
                Function.Call(Hash.SET_MAX_WANTED_LEVEL, (InputArgument)0);
                Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, (InputArgument)Game.Player.Handle, (InputArgument)true);
            }
            else
            {
                Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, (InputArgument)Game.Player.Handle, (InputArgument)false);
            }
        }
    }

    private void EveryoneIgnoreToggle_CheckboxChanged(object sender, EventArgs e)
    {
        everyoneIgnoreEnabled = ((NativeCheckboxItem)sender).Checked;

        if (everyoneIgnoreEnabled)
        {

            Function.Call(Hash.SET_EVERYONE_IGNORE_PLAYER, Game.Player.Handle, true);
        }
        else
        {

            Function.Call(Hash.SET_EVERYONE_IGNORE_PLAYER, Game.Player.Handle, false);
        }
    }

    private void ToggleInvisibility()
    {
        invisibilityEnabled = !invisibilityEnabled;

      
        Function.Call(Hash.SET_ENTITY_VISIBLE, Game.Player.Character.Handle, !invisibilityEnabled);
    }

    private void ToggleInfiniteStamina()
    {
        infiniteStaminaEnabled = !infiniteStaminaEnabled;

       
        if (infiniteStaminaEnabled)
        {
            Function.Call(Hash.RESET_PLAYER_STAMINA, Game.Player.Handle);
            Function.Call(Hash.RESTORE_PLAYER_STAMINA, Game.Player.Handle, 1.0f);
        }
        
    }




    private bool InfiniteSpecialAbilityToggle
    {
        get { return infiniteSpecialAbilityEnabled; }
        set
        {
            if (value)
            {
                SetInfiniteSpecialAbility();
            }
            else
            {
                ResetInfiniteSpecialAbility();
            }

            infiniteSpecialAbilityEnabled = value;
        }
    }

    private void SetInfiniteSpecialAbility()
    {
        Player player = Game.Player;


        Function.Call(Hash.SPECIAL_ABILITY_FILL_METER, player, true, 0);
    }

    private void ResetInfiniteSpecialAbility()
    {
        Player player = Game.Player;


        Function.Call(Hash.SPECIAL_ABILITY_FILL_METER, player, false, 0);
    }

    private void GainWantedLevel()
    {
        
        int currentWantedLevel = Function.Call<int>(Hash.GET_PLAYER_WANTED_LEVEL, Game.Player.Handle);

        Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, Game.Player.Handle, currentWantedLevel + 1, false);

        Function.Call(Hash.SET_PLAYER_WANTED_LEVEL_NOW, Game.Player.Handle, false);
    }


    private void RemoveWantedLevel()
    {
      
        int currentWantedLevel = Function.Call<int>(Hash.GET_PLAYER_WANTED_LEVEL, Game.Player.Handle);

     
        int newWantedLevel = Math.Max(0, currentWantedLevel - 1);

       
        Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, Game.Player.Handle, newWantedLevel, false);

        
        Function.Call(Hash.SET_PLAYER_WANTED_LEVEL_NOW, Game.Player.Handle, false);
    }


    private void HealPlayerAction_Activated(object sender, EventArgs e)
    {
        Ped character = Game.Player.Character;
        character.Health = character.MaxHealth;
        character.Armor = 100;
    }

    private void CleanPlayerAction_Activated(object sender, EventArgs e)
    {
        Ped character = Game.Player.Character;
        Function.Call(Hash.CLEAR_PED_BLOOD_DAMAGE, (InputArgument)character.Handle);
        Function.Call(Hash.CLEAR_PED_DAMAGE_DECAL_BY_ZONE, (InputArgument)character.Handle, (InputArgument)10, (InputArgument)"ALL");
    }

    private void Suicide()
    {
 
        Function.Call(Hash.SET_ENTITY_HEALTH, Game.Player.Character.Handle, 0);
    }

    // Weapons

    private void GiveAllWeapons()
    {
        Ped player = Game.Player.Character;

        foreach (WeaponHash weaponHash in Enum.GetValues(typeof(WeaponHash)))
        {
            Function.Call(Hash.GIVE_WEAPON_TO_PED, player.Handle, (int)weaponHash, 999, false, true);
        }
    }

        private void MaxAmmo()
    {
       
        foreach (var weaponHash in Enum.GetValues(typeof(WeaponHash)).Cast<WeaponHash>())
        {
            Function.Call(Hash.SET_PED_AMMO, Game.Player.Character.Handle, weaponHash, 9999);
        }
    }

    private void InfiniteAmmoToggle_CheckboxChanged(object sender, EventArgs e)
    {
        bool infiniteAmmoEnabled = ((NativeCheckboxItem)sender).Checked;

        if (Game.Player.Character != null)
        {
            if (infiniteAmmoEnabled)
            {
                Function.Call(Hash.SET_PED_INFINITE_AMMO, Game.Player.Character.Handle, true, 0);
            }
            else
            {
                Function.Call(Hash.SET_PED_INFINITE_AMMO, Game.Player.Character.Handle, false, 0);
            }
        }
    }

    private void NoReloadToggle_CheckboxChanged(object sender, EventArgs e)
    {
        noReloadEnabled = ((NativeCheckboxItem)sender).Checked;

        if (noReloadEnabled)
        {

            Function.Call(Hash.SET_PED_INFINITE_AMMO_CLIP, Game.Player.Character.Handle, true);
        }
        else
        {

            Function.Call(Hash.SET_PED_INFINITE_AMMO_CLIP, Game.Player.Character.Handle, false);
        }
    }


    private bool RainbowGunToggle
    {
        get { return rainbowGunEnabled; }
        set
        {
            rainbowGunEnabled = value;
            if (rainbowGunEnabled)
            {
                Tick += RainbowGunTick;
            }
            else
            {
                Tick -= RainbowGunTick;
            }
        }
    }

    public List<VehicleInfo> VehicleList { get => vehicleList; set => vehicleList = value; }

    private void RainbowGunTick(object sender, EventArgs e)
    {
        if (Game.Player.Character.Weapons.Current != null)
        {
            rainbowGunTickCounter++;
            if (rainbowGunTickCounter >= RainbowGunTickDelay)
            {
                rainbowGunTickCounter = 0;
                currentTintIndex = (currentTintIndex + 1) % 8;
                Function.Call(Hash.SET_PED_WEAPON_TINT_INDEX, Game.Player.Character.Handle, Game.Player.Character.Weapons.Current.Hash, currentTintIndex);
            }
        }
    }


    // Vehicles

    private void VehicleGodModeToggle_CheckboxChanged(object sender, EventArgs e)
    {

        this.vehicleGodMode = ((NativeCheckboxItem)sender).Checked;


        ToggleVehicleGodMode();
    }


    private void ToggleVehicleGodMode()
    {
        if (Game.Player.Character.IsInVehicle() && Game.Player.Character.CurrentVehicle != null)
        {
            Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;

            if (this.vehicleGodMode)
            {
                Function.Call(Hash.SET_ENTITY_INVINCIBLE, currentVehicle.Handle, true);
            }
            else
            {
                Function.Call(Hash.SET_ENTITY_INVINCIBLE, currentVehicle.Handle, false);
            }
        }
    }


    private void FixVehicle()
    {
        if (Game.Player.Character.IsInVehicle())
        {
            Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
            int healthBeforeFix = currentVehicle.Health;
            currentVehicle.Repair();
            currentVehicle.Health = healthBeforeFix;
        }
        else
        {
            Notification.Show("You Are Not In A Vehicle");
        }
    }

    private void CleanVehicle()
    {
        if (Game.Player.Character.IsInVehicle() && Game.Player.Character.CurrentVehicle != null)
        {
            Function.Call(Hash.SET_VEHICLE_DIRT_LEVEL, Game.Player.Character.CurrentVehicle.Handle, 0.0f);
        }
        else
        {
            Notification.Show("You Need To Be In A Vehicle");
        }
    }

    private void ApplyDirtVehicle()
    {
        if (Game.Player.Character.IsInVehicle() && Game.Player.Character.CurrentVehicle != null)
        {

            Function.Call(Hash.SET_VEHICLE_DIRT_LEVEL, Game.Player.Character.CurrentVehicle.Handle, 15.0f);
        }
        else
        {
            Notification.Show("You Need To Be In A vehicle");
        }
    }

    // Spawner 



    private List<VehicleInfo> vehicleList = new List<VehicleInfo>
{        
   // Boats

    new VehicleInfo { Name = "avisa", Hash = (VehicleHash)0x9A474B5E },
    new VehicleInfo { Name = "Dinghy", Hash = (VehicleHash)0x3D961290 },
    new VehicleInfo { Name = "dinghy2", Hash = (VehicleHash)0x107F392C },
    new VehicleInfo { Name = "dinghy3", Hash = (VehicleHash)0x1E5E54EA },
    new VehicleInfo { Name = "dinghy4", Hash = (VehicleHash)0x33B47F96 },
    new VehicleInfo { Name = "dinghy5", Hash = (VehicleHash)0xC58DA34A },
    new VehicleInfo { Name = "jetmax", Hash = (VehicleHash)0x33581161 },
    new VehicleInfo { Name = "kosatka", Hash = (VehicleHash)0x4FAF0D70 },
    new VehicleInfo { Name = "longfin", Hash = (VehicleHash)0x6EF89CCC },
    new VehicleInfo { Name = "marquis", Hash = (VehicleHash)0xC1CE1183 },
    new VehicleInfo { Name = "patrolboat", Hash = (VehicleHash)0xEF813606 },
    new VehicleInfo { Name = "Predator", Hash = (VehicleHash)0xE2E7D4AB },
    new VehicleInfo { Name = "seashark", Hash = (VehicleHash)0xC2974024 },
    new VehicleInfo { Name = "seashark2", Hash = (VehicleHash)0xDB4388E4 },
    new VehicleInfo { Name = "seashark3", Hash = (VehicleHash)0xED762D49 },
    new VehicleInfo { Name = "speeder", Hash = (VehicleHash)0xDC60D2B },
    new VehicleInfo { Name = "speeder2", Hash = (VehicleHash)0x1A144F2A },
    new VehicleInfo { Name = "squalo", Hash = (VehicleHash)0x17DF5EC2 },
    new VehicleInfo { Name = "submersible", Hash = (VehicleHash)0x2DFF622F },
    new VehicleInfo { Name = "submersible2", Hash = (VehicleHash)0xC07107EE },
    new VehicleInfo { Name = "Suntrap", Hash = (VehicleHash)0xEF2295C9 },
    new VehicleInfo { Name = "toro", Hash = (VehicleHash)0x3FD5AA2F },
    new VehicleInfo { Name = "toro2", Hash = (VehicleHash)0x362CAC6D },
    new VehicleInfo { Name = "tropic", Hash = (VehicleHash)0x1149422F },
    new VehicleInfo { Name = "tropic2", Hash = (VehicleHash)0x56590FE9 },
    new VehicleInfo { Name = "tug", Hash = (VehicleHash)0x82CAC433 },

    // Commercial

    new VehicleInfo { Name = "Benson", Hash = (VehicleHash)0x7A61B330 },
    new VehicleInfo { Name = "Biff", Hash = (VehicleHash)0x32B91AE8 },
    new VehicleInfo { Name = "cerberus", Hash = (VehicleHash)0xD039510B },
    new VehicleInfo { Name = "cerberus2", Hash = (VehicleHash)0x287FA449 },
    new VehicleInfo { Name = "cerberus3", Hash = (VehicleHash)0x71D3B6F0 },
    new VehicleInfo { Name = "Hauler", Hash = (VehicleHash)0x5A82F9AE },
    new VehicleInfo { Name = "Hauler2", Hash = (VehicleHash)0x171C92C4 },
    new VehicleInfo { Name = "Mule", Hash = (VehicleHash)0x35ED670B },
    new VehicleInfo { Name = "Mule2", Hash = (VehicleHash)0xC1632BEB },
    new VehicleInfo { Name = "Mule3", Hash = (VehicleHash)0x85A5B471 },
    new VehicleInfo { Name = "mule4", Hash = (VehicleHash)0x73F4110E },
    new VehicleInfo { Name = "mule5", Hash = (VehicleHash)0x501AC93C },
    new VehicleInfo { Name = "Packer", Hash = (VehicleHash)0x21EEE87D },
    new VehicleInfo { Name = "Phantom", Hash = (VehicleHash)0x809AA4CB },
    new VehicleInfo { Name = "phantom2", Hash = (VehicleHash)0x9DAE1398 },
    new VehicleInfo { Name = "phantom3", Hash = (VehicleHash)0xA90ED5C },
    new VehicleInfo { Name = "Pounder", Hash = (VehicleHash)0x7DE35E7D },
    new VehicleInfo { Name = "pounder2", Hash = (VehicleHash)0x6290F15B },
    new VehicleInfo { Name = "stockade", Hash = (VehicleHash)0x6827CF72 },
    new VehicleInfo { Name = "stockade3", Hash = (VehicleHash)0xF337AB36 },
    new VehicleInfo { Name = "terbyte", Hash = (VehicleHash)0x897AFC65 },

    // Compact

    new VehicleInfo { Name = "Asbo", Hash = (VehicleHash)0x42ACA95F },
    new VehicleInfo { Name = "Blista", Hash = (VehicleHash)0xEB70965F },
    new VehicleInfo { Name = "Brioso", Hash = (VehicleHash)0x5C55CB39 },
    new VehicleInfo { Name = "Brioso2", Hash = (VehicleHash)0x55365079 },
    new VehicleInfo { Name = "Brioso3", Hash = (VehicleHash)0xE827DE },
    new VehicleInfo { Name = "Club", Hash = (VehicleHash)0x82E47E85 },
    new VehicleInfo { Name = "Dilettante", Hash = (VehicleHash)0xBC993509 },
    new VehicleInfo { Name = "Dilettante2", Hash = (VehicleHash)0x64430650 },
    new VehicleInfo { Name = "Issi2", Hash = (VehicleHash)0xB9CB3B69 },
    new VehicleInfo { Name = "Issi3", Hash = (VehicleHash)0x378236E1 },
    new VehicleInfo { Name = "Issi4", Hash = (VehicleHash)0x256E92BA },
    new VehicleInfo { Name = "Issi5", Hash = (VehicleHash)0x5BA0FF1E },
    new VehicleInfo { Name = "Issi6", Hash = (VehicleHash)0x49E25BA1 },
    new VehicleInfo { Name = "Kanjo", Hash = (VehicleHash)0x18619B7E },
    new VehicleInfo { Name = "Panto", Hash = (VehicleHash)0xE644E480 },
    new VehicleInfo { Name = "Prairie", Hash = (VehicleHash)0xA988D3A2 },
    new VehicleInfo { Name = "Rhapsody", Hash = (VehicleHash)0x322CF98F },
    new VehicleInfo { Name = "Weevil", Hash = (VehicleHash)0x61FE4D6A },

    // Coupe 

    new VehicleInfo { Name = "cogcabrio", Hash = (VehicleHash)0x13B57D8A },
    new VehicleInfo { Name = "exemplar", Hash = (VehicleHash)0xFFB15B5E },
    new VehicleInfo { Name = "f620", Hash = (VehicleHash)0xDCBCBE48 },
    new VehicleInfo { Name = "felon", Hash = (VehicleHash)0xE8A8BDA8 },
    new VehicleInfo { Name = "felon2", Hash = (VehicleHash)0xFAAD85EE },
    new VehicleInfo { Name = "jackal", Hash = (VehicleHash)0xDAC67112 },
    new VehicleInfo { Name = "kanjosj", Hash = (VehicleHash)0xFC2E479A },
    new VehicleInfo { Name = "oracle", Hash = (VehicleHash)0x506434F6 },
    new VehicleInfo { Name = "oracle2", Hash = (VehicleHash)0xE18195B2 },
    new VehicleInfo { Name = "postlude", Hash = (VehicleHash)0xEE6F8F79 },
    new VehicleInfo { Name = "previon", Hash = (VehicleHash)0x546DA331 },
    new VehicleInfo { Name = "sentinel", Hash = (VehicleHash)0x50732C82 },
    new VehicleInfo { Name = "sentinel2", Hash = (VehicleHash)0x3412AE2D },
    new VehicleInfo { Name = "windsor", Hash = (VehicleHash)0x5E4327C8 },
    new VehicleInfo { Name = "windsor2", Hash = (VehicleHash)0x8CF5CAE1 },
    new VehicleInfo { Name = "zion", Hash = (VehicleHash)0xBD1B39C3 },
    new VehicleInfo { Name = "zion2", Hash = (VehicleHash)0xB8E2AE18 },

    // Cycyle

     new VehicleInfo { Name = "BMX", Hash = (VehicleHash)0x43779C54 },
    new VehicleInfo { Name = "cruiser", Hash = (VehicleHash)0x1ABA13B5 },
    new VehicleInfo { Name = "fixter", Hash = (VehicleHash)0xCE23D3BF },
    new VehicleInfo { Name = "scorcher", Hash = (VehicleHash)0xF4E1AA15 },
    new VehicleInfo { Name = "tribike", Hash = (VehicleHash)0x4339CD69 },
    new VehicleInfo { Name = "tribike2", Hash = (VehicleHash)0xB67597EC },
    new VehicleInfo { Name = "tribike3", Hash = (VehicleHash)0xE823FB48 },

    // Emergency 

    new VehicleInfo { Name = "ambulance", Hash = (VehicleHash)0x45D56ADA },
    new VehicleInfo { Name = "FBI", Hash = (VehicleHash)0x432EA949 },
    new VehicleInfo { Name = "FBI2", Hash = (VehicleHash)0x9DC66994 },
    new VehicleInfo { Name = "firetruk", Hash = (VehicleHash)0x73920F8E },
    new VehicleInfo { Name = "lguard", Hash = (VehicleHash)0x1BF8D381 },
    new VehicleInfo { Name = "pbus", Hash = (VehicleHash)0x885F3671 },
    new VehicleInfo { Name = "police", Hash = (VehicleHash)0x79FBB0C5 },
    new VehicleInfo { Name = "police2", Hash = (VehicleHash)0x9F05F101 },
    new VehicleInfo { Name = "police3", Hash = (VehicleHash)0x71FA16EA },
    new VehicleInfo { Name = "police4", Hash = (VehicleHash)0x8A63C7B9 },
    new VehicleInfo { Name = "policeb", Hash = (VehicleHash)0xFDEFAEC3 },
    new VehicleInfo { Name = "policeold1", Hash = (VehicleHash)0xA46462F7 },
    new VehicleInfo { Name = "policeold2", Hash = (VehicleHash)0x95F4C618 },
    new VehicleInfo { Name = "policet", Hash = (VehicleHash)0x1B38E955 },
    new VehicleInfo { Name = "pRanger", Hash = (VehicleHash)0x2C33B46E },
    new VehicleInfo { Name = "riot", Hash = (VehicleHash)0xB822A1AA },
    new VehicleInfo { Name = "riot2", Hash = (VehicleHash)0x9B16A3B4 },
    new VehicleInfo { Name = "SHERIFF", Hash = (VehicleHash)0x9BAA707C },
    new VehicleInfo { Name = "sheriff2", Hash = (VehicleHash)0x72935408 },

    // Helicopters

    new VehicleInfo { Name = "Akula", Hash = (VehicleHash)0x46699F47 },
    new VehicleInfo { Name = "Annihilator", Hash = (VehicleHash)0x31F0B376 },
    new VehicleInfo { Name = "Annihilator Stealth", Hash = (VehicleHash)0x11962E49 },
    new VehicleInfo { Name = "Buzzard Attack Chopper", Hash = (VehicleHash)0x2F03547B },
    new VehicleInfo { Name = "Buzzard", Hash = (VehicleHash)0x2C75F0DD },
    new VehicleInfo { Name = "Cargobob", Hash = (VehicleHash)0xFCFCB68B },
    new VehicleInfo { Name = "Cargobob", Hash = (VehicleHash)0x60A7EA10 },
    new VehicleInfo { Name = "Cargobob", Hash = (VehicleHash)0x53174EEF },
    new VehicleInfo { Name = "Cargobob", Hash = (VehicleHash)0x78BC1A3C },
    new VehicleInfo { Name = "Conada", Hash = (VehicleHash)0xE384DD25 },
    new VehicleInfo { Name = "Frogger", Hash = (VehicleHash)0x2C634FBD },
    new VehicleInfo { Name = "Frogger", Hash = (VehicleHash)0x742E9AC0 },
    new VehicleInfo { Name = "Havok", Hash = (VehicleHash)0x89BA59F5 },
    new VehicleInfo { Name = "FH-1 Hunter", Hash = (VehicleHash)0xFD707EDE },
    new VehicleInfo { Name = "Maverick", Hash = (VehicleHash)0x9D0450CA },
    new VehicleInfo { Name = "Police Maverick", Hash = (VehicleHash)0x1517D4D9 },
    new VehicleInfo { Name = "Savage", Hash = (VehicleHash)0xFB133A17 },
    new VehicleInfo { Name = "Sea Sparrow", Hash = (VehicleHash)0xD4AE63D9 },
    new VehicleInfo { Name = "Sparrow", Hash = (VehicleHash)0x494752F7 },
    new VehicleInfo { Name = "Sparrow", Hash = (VehicleHash)0x5F017E6B },
    new VehicleInfo { Name = "Skylift", Hash = (VehicleHash)0x3E48BF23 },
    new VehicleInfo { Name = "SuperVolito", Hash = (VehicleHash)0x2A54C47D },
    new VehicleInfo { Name = "SuperVolito Carbon", Hash = (VehicleHash)0x9C5E5644 },
    new VehicleInfo { Name = "Swift", Hash = (VehicleHash)0xEBC24DF2 },
    new VehicleInfo { Name = "Swift Deluxe", Hash = (VehicleHash)0x4019CB4C },
    new VehicleInfo { Name = "Valkyrie", Hash = (VehicleHash)0xA09E15FD },
    new VehicleInfo { Name = "Valkyrie MOD.0", Hash = (VehicleHash)0x5BFA5C4B },
    new VehicleInfo { Name = "Volatus", Hash = (VehicleHash)0x920016F1 },

    // Industrial 

    new VehicleInfo { Name = "Bulldozer", Hash = (VehicleHash)0x7074F39D },
    new VehicleInfo { Name = "Cutter", Hash = (VehicleHash)0xC3FBA120 },
    new VehicleInfo { Name = "Dump", Hash = (VehicleHash)0x810369E2 },
    new VehicleInfo { Name = "Flatbed", Hash = (VehicleHash)0x50B0215A },
    new VehicleInfo { Name = "Guardian", Hash = (VehicleHash)0x825A9F4C },
    new VehicleInfo { Name = "Dock Handler", Hash = (VehicleHash)0x1A7FCEFA },
    new VehicleInfo { Name = "Mixer", Hash = (VehicleHash)0xD138A6BB },
    new VehicleInfo { Name = "Mixer", Hash = (VehicleHash)0x1C534995 },
    new VehicleInfo { Name = "Rubble", Hash = (VehicleHash)0x9A5B1DCC },
    new VehicleInfo { Name = "Tipper", Hash = (VehicleHash)0x2E19879 },
    new VehicleInfo { Name = "Tipper", Hash = (VehicleHash)0xC7824E5E },

     // Military

    new VehicleInfo { Name = "APC", Hash = (VehicleHash)0x2189D250 },
    new VehicleInfo { Name = "Barracks", Hash = (VehicleHash)0xCEEA3F4B },
    new VehicleInfo { Name = "Barracks Semi", Hash = (VehicleHash)0x4008EABB },
    new VehicleInfo { Name = "Barracks", Hash = (VehicleHash)0x2592B5CF },
    new VehicleInfo { Name = "Barrage", Hash = (VehicleHash)0xF34DFB25 },
    new VehicleInfo { Name = "Chernobog", Hash = (VehicleHash)0xD6BC7523 },
    new VehicleInfo { Name = "Crusader", Hash = (VehicleHash)0x132D5A1A },
    new VehicleInfo { Name = "Half-track", Hash = (VehicleHash)0xFE141DA6 },
    new VehicleInfo { Name = "TM-02 Khanjali", Hash = (VehicleHash)0xAA6F980A },
    new VehicleInfo { Name = "Invade and Persuade Tank", Hash = (VehicleHash)0xB53C6C52 },
    new VehicleInfo { Name = "Rhino Tank", Hash = (VehicleHash)0x2EA68690 },
    new VehicleInfo { Name = "Apocalypse Scarab", Hash = (VehicleHash)0xBBA2A2F7 },
    new VehicleInfo { Name = "Future Shock Scarab", Hash = (VehicleHash)0x5BEB3CE0 },
    new VehicleInfo { Name = "Nightmare Scarab", Hash = (VehicleHash)0xDD71BFEB },
    new VehicleInfo { Name = "Thruster", Hash = (VehicleHash)0x58CDAF30 },
    new VehicleInfo { Name = "Anti-Aircraft Trailer", Hash = (VehicleHash)0x8FD54EBB },
    new VehicleInfo { Name = "Vetir", Hash = (VehicleHash)0x780FFBD2 },

      // Motorcycles

    new VehicleInfo { Name = "Akuma", Hash = (VehicleHash)0x63ABADE7 },
    new VehicleInfo { Name = "Avarus", Hash = (VehicleHash)0x81E38F7F },
    new VehicleInfo { Name = "Bagger", Hash = (VehicleHash)0x806B9CC3 },
    new VehicleInfo { Name = "Bati 801", Hash = (VehicleHash)0xF9300CC5 },
    new VehicleInfo { Name = "Bati 801RR", Hash = (VehicleHash)0xCADD5D2D },
    new VehicleInfo { Name = "BF400", Hash = (VehicleHash)0x5283265 },
    new VehicleInfo { Name = "Carbon RS", Hash = (VehicleHash)0xABB0C0 },
    new VehicleInfo { Name = "Chimera", Hash = (VehicleHash)0x675ED7 },
    new VehicleInfo { Name = "Cliffhanger", Hash = (VehicleHash)0x17420102 },
    new VehicleInfo { Name = "Daemon", Hash = (VehicleHash)0x77934CEE },
    new VehicleInfo { Name = "Daemon", Hash = (VehicleHash)0xAC4E93C9 },
    new VehicleInfo { Name = "Apocalypse Deathbike", Hash = (VehicleHash)0xFE5F0722 },
    new VehicleInfo { Name = "Future Shock Deathbike", Hash = (VehicleHash)0x93F09558 },
    new VehicleInfo { Name = "Nightmare Deathbike", Hash = (VehicleHash)0xAE12C99C },
    new VehicleInfo { Name = "Defiler", Hash = (VehicleHash)0x30FF0190 },
    new VehicleInfo { Name = "Diabolus", Hash = (VehicleHash)0xF1B44F44 },
    new VehicleInfo { Name = "Diabolus Custom", Hash = (VehicleHash)0x6ABDF65E },
    new VehicleInfo { Name = "Double-T", Hash = (VehicleHash)0x9C669788 },
    new VehicleInfo { Name = "Enduro", Hash = (VehicleHash)0x6882FA73 },
    new VehicleInfo { Name = "Esskey", Hash = (VehicleHash)0x794CB30C },
    new VehicleInfo { Name = "Faggio Sport", Hash = (VehicleHash)0x9229E4EB },
    new VehicleInfo { Name = "Faggio", Hash = (VehicleHash)0x350D1AB },
    new VehicleInfo { Name = "Faggio Mod", Hash = (VehicleHash)0xB328B188 },
    new VehicleInfo { Name = "FCR 1000", Hash = (VehicleHash)0x25676EAF },
    new VehicleInfo { Name = "FCR 1000 Custom", Hash = (VehicleHash)0xD2D5E00E },
    new VehicleInfo { Name = "Gargoyle", Hash = (VehicleHash)0x2C2C2324 },
    new VehicleInfo { Name = "Hakuchou", Hash = (VehicleHash)0x4B6C568A },
    new VehicleInfo { Name = "Hakuchou Drag", Hash = (VehicleHash)0xF0C2A91F },
    new VehicleInfo { Name = "Hexer", Hash = (VehicleHash)0x11F76C14 },
    new VehicleInfo { Name = "Innovation", Hash = (VehicleHash)0xF683EACA },
    new VehicleInfo { Name = "Lectro", Hash = (VehicleHash)0x26321E67 },
    new VehicleInfo { Name = "Manchez", Hash = (VehicleHash)0xA5325278 },
    new VehicleInfo { Name = "Manchez Scout", Hash = (VehicleHash)0x40C332A3 },
    new VehicleInfo { Name = "Manchez Scout C", Hash = (VehicleHash)0x5285D628 },
    new VehicleInfo { Name = "Nemesis", Hash = (VehicleHash)0xDA288376 },
    new VehicleInfo { Name = "Nightblade", Hash = (VehicleHash)0xA0438767 },
    new VehicleInfo { Name = "Oppressor", Hash = (VehicleHash)0x34B82784 },
    new VehicleInfo { Name = "Oppressor Mk II", Hash = (VehicleHash)0x7B54A9D3 },
    new VehicleInfo { Name = "PCJ 600", Hash = (VehicleHash)0xC9CEAF06 },
    new VehicleInfo { Name = "Powersurge", Hash = (VehicleHash)0xAD5E30D7 },
    new VehicleInfo { Name = "Rat Bike", Hash = (VehicleHash)0x6FACDF31 },
    new VehicleInfo { Name = "Reever", Hash = (VehicleHash)0x76D7C404 },
    new VehicleInfo { Name = "Rampant Rocket", Hash = (VehicleHash)0x36A167E0 },
    new VehicleInfo { Name = "Ruffian", Hash = (VehicleHash)0xCABD11E8 },
    new VehicleInfo { Name = "Sanchez (livery)", Hash = (VehicleHash)0x2EF89E46 },
    new VehicleInfo { Name = "Sanchez", Hash = (VehicleHash)0xA960B13E },
    new VehicleInfo { Name = "Sanctus", Hash = (VehicleHash)0x58E316C7 },
    new VehicleInfo { Name = "Shinobi", Hash = (VehicleHash)0x50A6FB9C },
    new VehicleInfo { Name = "Shotaro", Hash = (VehicleHash)0xE7D2A16E },
    new VehicleInfo { Name = "Sovereign", Hash = (VehicleHash)0x2C509634 },
    new VehicleInfo { Name = "Stryder", Hash = (VehicleHash)0x11F58A5A },
    new VehicleInfo { Name = "Thrust", Hash = (VehicleHash)0x6D6F8F43 },
    new VehicleInfo { Name = "Vader", Hash = (VehicleHash)0xF79A00F7 },
    new VehicleInfo { Name = "Vindicator", Hash = (VehicleHash)0xAF599F01 },
    new VehicleInfo { Name = "Vortex", Hash = (VehicleHash)0xDBA9DBFC },
    new VehicleInfo { Name = "Wolfsbane", Hash = (VehicleHash)0xDB20A373 },
    new VehicleInfo { Name = "Zombie Bobber", Hash = (VehicleHash)0xC3D7C72B },
    new VehicleInfo { Name = "Zombie Chopper", Hash = (VehicleHash)0xDE05FB87 },

    // Muscle

 

    new VehicleInfo { Name = "Blade", Hash = (VehicleHash)0xB820ED5E },
    new VehicleInfo { Name = "Broadway", Hash = (VehicleHash)0x8CC51028 },
    new VehicleInfo { Name = "Buccaneer", Hash = (VehicleHash)0xD756460C },
    new VehicleInfo{ Name = "Buccaneer Custom", Hash = (VehicleHash)0xC397F748 },
    new VehicleInfo { Name = "Buffalo STX", Hash = (VehicleHash)0xDB0C9B04 },
    new VehicleInfo { Name = "Chino", Hash = (VehicleHash)0x14D69010 },
    new VehicleInfo { Name = "Chino Custom", Hash = (VehicleHash)0xAED64A63 },
    new VehicleInfo { Name = "Clique", Hash = (VehicleHash)0xA29F78B0 },
    new VehicleInfo { Name = "Coquette BlackFin", Hash = (VehicleHash)0x2EC385FE },
    new VehicleInfo { Name = "Deviant", Hash = (VehicleHash)0x4C3FFF49 },
    new VehicleInfo { Name = "Dominator", Hash = (VehicleHash)0x4CE68AC },
    new VehicleInfo { Name = "Pisswasser Dominator", Hash = (VehicleHash)0xC96B73D9 },
    new VehicleInfo { Name = "Dominator GTX", Hash = (VehicleHash)0xC52C6B93 },
    new VehicleInfo { Name = "Apocalypse Dominator", Hash = (VehicleHash)0xD6FB0F30 },
    new VehicleInfo { Name = "Future Shock Dominator", Hash = (VehicleHash)0xAE0A3D4F },
    new VehicleInfo  { Name = "Nightmare Dominator", Hash = (VehicleHash)0xB2E046FB },
    new VehicleInfo { Name = "Dominator ASP", Hash = (VehicleHash)0x196F9418 },
    new VehicleInfo { Name = "Dominator GTT", Hash = (VehicleHash)0x2BE8B90A },
    new VehicleInfo { Name = "Dukes", Hash = (VehicleHash)0x2B26F456 },
    new VehicleInfo { Name = "Duke O'Death", Hash = (VehicleHash)0xEC8F7094 },
    new VehicleInfo { Name = "Beater Dukes", Hash = (VehicleHash)0x7F3415E3 },
    new VehicleInfo { Name = "Ellie", Hash = (VehicleHash)0xB472D2B5 },
    new VehicleInfo  { Name = "Eudora", Hash = (VehicleHash)0xB581BF9A },
    new VehicleInfo { Name = "Faction", Hash = (VehicleHash)0x81A9CDDF },
    new VehicleInfo { Name = "Faction Custom", Hash = (VehicleHash)0x95466BDB },
    new VehicleInfo { Name = "Faction Custom Donk", Hash = (VehicleHash)0x866BCE26 },
    new VehicleInfo { Name = "Gauntlet", Hash = (VehicleHash)0x94B395C5 },
    new VehicleInfo { Name = "Redwood Gauntlet", Hash = (VehicleHash)0x14D22159 },
    new VehicleInfo { Name = "Gauntlet Classic", Hash = (VehicleHash)0x2B0C4DCD },
    new VehicleInfo { Name = "Gauntlet Hellfire", Hash = (VehicleHash)0x734C5E50 },
    new VehicleInfo { Name = "Gauntlet Classic Custom", Hash = (VehicleHash)0x817AFAAD },
    new VehicleInfo { Name = "Greenwood", Hash = (VehicleHash)0x26ED430 },
    new VehicleInfo { Name = "Hermes", Hash = (VehicleHash)0xE83C17 },
    new VehicleInfo { Name = "Hotknife", Hash = (VehicleHash)0x239E390 },
    new VehicleInfo { Name = "Hustler", Hash = (VehicleHash)0x23CA25F2 },
    new VehicleInfo { Name = "Impaler", Hash = (VehicleHash)0x83070B62 },
    new VehicleInfo { Name = "Apocalypse Impaler", Hash = (VehicleHash)0x3C26BD0C },
    new VehicleInfo { Name = "Future Shock Impaler", Hash = (VehicleHash)0x8D45DF49 },
    new VehicleInfo { Name = "Nightmare Impaler", Hash = (VehicleHash)0x9804F4C7 },
    new VehicleInfo { Name = "Apocalypse Imperator", Hash = (VehicleHash)0x1A861243 },
    new VehicleInfo { Name = "Future Shock Imperator", Hash = (VehicleHash)0x619C1B82 },
    new VehicleInfo { Name = "Nightmare Imperator", Hash = (VehicleHash)0xD2F77E37 },
    new VehicleInfo { Name = "Lurcher", Hash = (VehicleHash)0x7B47A6A7 },
    new VehicleInfo { Name = "Manana Custom", Hash = (VehicleHash)0x665F785D },
    new VehicleInfo { Name = "Moonbeam", Hash = (VehicleHash)0x1F52A43F },
    new VehicleInfo { Name = "Moonbeam Custom", Hash = (VehicleHash)0x710A2B9B },
    new VehicleInfo { Name = "Nightshade", Hash = (VehicleHash)0x8C2BD0DC },
    new VehicleInfo { Name = "Peyote Gasser", Hash = (VehicleHash)0x9472CD24 },
    new VehicleInfo { Name = "Phoenix", Hash = (VehicleHash)0x831A21D5 },
    new VehicleInfo { Name = "Picador", Hash = (VehicleHash)0x59E0FBF3 },
    new VehicleInfo { Name = "Rat-Loader", Hash = (VehicleHash)0xD83C13CE },
    new VehicleInfo { Name = "Rat-Truck", Hash = (VehicleHash)0xDCE1D9F7 },
    new VehicleInfo { Name = "Ruiner", Hash = (VehicleHash)0xF26CEFF9 },
    new VehicleInfo { Name = "Ruiner 2000", Hash = (VehicleHash)0x381E10BD },
    new VehicleInfo { Name = "Ruiner", Hash = (VehicleHash)0x2E5AFD37 },
    new VehicleInfo { Name = "Ruiner ZZ-8", Hash = (VehicleHash)0x65BDEBFC },
    new VehicleInfo { Name = "Sabre Turbo", Hash = (VehicleHash)0x9B909C94 },
    new VehicleInfo { Name = "Sabre Turbo Custom", Hash = (VehicleHash)0xD4EA603 },
    new VehicleInfo { Name = "Slamvan", Hash = (VehicleHash)0x2B7F9DE3 },
    new VehicleInfo { Name = "Lost Slamvan", Hash = (VehicleHash)0x31ADBBFC },
    new VehicleInfo { Name = "Slamvan Custom", Hash = (VehicleHash)0x42BC5E19 },
    new VehicleInfo { Name = "Apocalypse Slamvan", Hash = (VehicleHash)0x8526E2F5 },
    new VehicleInfo { Name = "Future Shock Slamvan", Hash = (VehicleHash)0x163F8520 },
    new VehicleInfo { Name = "Nightmare Slamvan", Hash = (VehicleHash)0x67D52852 },
    new VehicleInfo { Name = "Stallion", Hash = (VehicleHash)0x72A4C31E },
    new VehicleInfo { Name = "Burger Shot Stallion", Hash = (VehicleHash)0xE80F67EE },
    new VehicleInfo { Name = "Tahoma Coupe", Hash = (VehicleHash)0xE478B977 },
    new VehicleInfo { Name = "Tampa", Hash = (VehicleHash)0x39F9C898 },
    new VehicleInfo { Name = "Weaponized Tampa", Hash = (VehicleHash)0xB7D9F7F1 },
    new VehicleInfo { Name = "Tulip", Hash = (VehicleHash)0x56D42971 },
    new VehicleInfo { Name = "Tulip M-100", Hash = (VehicleHash)0x1004EDA4 },
    new VehicleInfo { Name = "Vamos", Hash = (VehicleHash)0xFD128DFD },
    new VehicleInfo { Name = "Vigero", Hash = (VehicleHash)0xCEC6B9B7 },
    new VehicleInfo { Name = "Vigero ZX", Hash = (VehicleHash)0x973141FC },
    new VehicleInfo { Name = "Virgo", Hash = (VehicleHash)0xE2504942 },
    new VehicleInfo { Name = "Virgo Classic Custom", Hash = (VehicleHash)0xCA62927A },
    new VehicleInfo { Name = "Virgo Classic", Hash = (VehicleHash)0xFDFFB0 },
    new VehicleInfo { Name = "Voodoo Custom", Hash = (VehicleHash)0x779B4F2D },
    new VehicleInfo { Name = "Voodoo", Hash = (VehicleHash)0x1F3766E3 },
    new VehicleInfo { Name = "Weevil Custom", Hash = (VehicleHash)0xC4BB1908 },
    new VehicleInfo { Name = "Yosemite", Hash = (VehicleHash)0x6F946279 },
    new VehicleInfo { Name = "Drift Yosemite", Hash = (VehicleHash)0x64F49967 },

     // Off-Road

    new VehicleInfo { Name = "BfInjection", Hash = (VehicleHash)0x432AA566 },
    new VehicleInfo { Name = "bifta", Hash = (VehicleHash)0xEB298297 },
    new VehicleInfo { Name = "blazer", Hash = (VehicleHash)0x8125BCF9 },
    new VehicleInfo { Name = "blazer2", Hash = (VehicleHash)0xFD231729 },
    new VehicleInfo { Name = "blazer3", Hash = (VehicleHash)0xB44F0582 },
    new VehicleInfo { Name = "blazer4", Hash = (VehicleHash)0xE5BA6858 },
    new VehicleInfo { Name = "blazer5", Hash = (VehicleHash)0xA1355F67 },
    new VehicleInfo { Name = "Bodhi2", Hash = (VehicleHash)0xAA699BB6 },
    new VehicleInfo { Name = "boor", Hash = (VehicleHash)0x3B639C8D },
    new VehicleInfo { Name = "brawler", Hash = (VehicleHash)0xA7CE1BC5 },
    new VehicleInfo { Name = "bruiser", Hash = (VehicleHash)0x27D79225 },
    new VehicleInfo { Name = "bruiser2", Hash = (VehicleHash)0x9B065C9E },
    new VehicleInfo { Name = "bruiser3", Hash = (VehicleHash)0x8644331A },
    new VehicleInfo { Name = "brutus", Hash = (VehicleHash)0x7F81A829 },
    new VehicleInfo { Name = "brutus2", Hash = (VehicleHash)0x8F49AE28 },
    new VehicleInfo { Name = "brutus3", Hash = (VehicleHash)0x798682A2 },
    new VehicleInfo { Name = "caracara", Hash = (VehicleHash)0x4ABEBF23 },
    new VehicleInfo { Name = "caracara2", Hash = (VehicleHash)0xAF966F3C },
    new VehicleInfo { Name = "dloader", Hash = (VehicleHash)0x698521E3 },
    new VehicleInfo { Name = "draugur", Hash = (VehicleHash)0xD235A4A6 },
    new VehicleInfo { Name = "dubsta3", Hash = (VehicleHash)0xB6410173 },
    new VehicleInfo { Name = "dune", Hash = (VehicleHash)0x9CF21E0F },
    new VehicleInfo { Name = "dune2", Hash = (VehicleHash)0x1FD824AF },
    new VehicleInfo { Name = "dune3", Hash = (VehicleHash)0x711D4738 },
    new VehicleInfo { Name = "dune4", Hash = (VehicleHash)0xCEB28249 },
    new VehicleInfo { Name = "dune5", Hash = (VehicleHash)0xED62BFA9 },
    new VehicleInfo { Name = "everon", Hash = (VehicleHash)0x97553C28 },
    new VehicleInfo { Name = "freecrawler", Hash = (VehicleHash)0xFCC2F483 },
    new VehicleInfo { Name = "hellion", Hash = (VehicleHash)0xEA6A047F },
    new VehicleInfo { Name = "insurgent", Hash = (VehicleHash)0x9114EADA },
    new VehicleInfo { Name = "insurgent2", Hash = (VehicleHash)0x7B7E56F0 },
    new VehicleInfo { Name = "insurgent3", Hash = (VehicleHash)0x8D4B7A8A },
    new VehicleInfo { Name = "kalahari", Hash = (VehicleHash)0x5852838 },
    new VehicleInfo { Name = "kamacho", Hash = (VehicleHash)0xF8C2E0E7 },
    new VehicleInfo { Name = "marshall", Hash = (VehicleHash)0x49863E9C },
    new VehicleInfo { Name = "menacer", Hash = (VehicleHash)0x79DD18AE },
    new VehicleInfo { Name = "MESA3", Hash = (VehicleHash)0x84F42E51 },
    new VehicleInfo { Name = "monster", Hash = (VehicleHash)0xCD93A7DB },
    new VehicleInfo { Name = "monster3", Hash = (VehicleHash)0x669EB40A },
    new VehicleInfo { Name = "monster4", Hash = (VehicleHash)0x32174AFC},
    new VehicleInfo { Name = "monster5", Hash = (VehicleHash)0xD556917C},
    new VehicleInfo { Name = "nightshark", Hash = (VehicleHash)0x19DD9ED1},
    new VehicleInfo { Name = "outlaw", Hash = (VehicleHash)0x185E2FF3},
    new VehicleInfo { Name = "patriot3", Hash = (VehicleHash)0xD80F4A44 },
    new VehicleInfo { Name = "RancherXL", Hash = (VehicleHash)0x6210CBB0 },
    new VehicleInfo { Name = "rancherxl2", Hash = (VehicleHash)0x7341576B },
    new VehicleInfo { Name = "rcbandito", Hash = (VehicleHash)0xEEF345EC},
    new VehicleInfo { Name = "Rebel", Hash = (VehicleHash)0xB802DD46 },
    new VehicleInfo { Name = "rebel2", Hash = (VehicleHash)0x8612B64B },
    new VehicleInfo { Name = "riata", Hash = (VehicleHash)0xA4A4E453},
    new VehicleInfo { Name = "sandking", Hash = (VehicleHash)0xB9210FD0 },
    new VehicleInfo { Name = "sandking2", Hash = (VehicleHash)0x3AF8C345 },
    new VehicleInfo { Name = "technical", Hash = (VehicleHash)0x83051506 },
    new VehicleInfo { Name = "technical2", Hash = (VehicleHash)0x4662BCBB },
    new VehicleInfo { Name = "technical3", Hash = (VehicleHash)0x50D4D19F},
    new VehicleInfo { Name = "trophytruck", Hash = (VehicleHash)0x612F4B6},
    new VehicleInfo { Name = "trophytruck2", Hash = (VehicleHash)0xD876DBE2},
    new VehicleInfo { Name = "vagrant", Hash = (VehicleHash)0x2C1FEA99},
    new VehicleInfo { Name = "verus", Hash = (VehicleHash)0x11CBC051 },
    new VehicleInfo { Name = "winky", Hash = (VehicleHash)0xF376F1E },
    new VehicleInfo { Name = "yosemite3", Hash = (VehicleHash)0x409D787},
    new VehicleInfo { Name = "zhaba", Hash = (VehicleHash)0x4C8DBA51},

    // Open Wheel

    new VehicleInfo { Name = "formula", Hash = (VehicleHash)0x1446590A },
    new VehicleInfo { Name = "formula2", Hash = (VehicleHash)0x8B213907 },
    new VehicleInfo { Name = "openwheel1", Hash = (VehicleHash)0x58F77553 },
    new VehicleInfo { Name = "openwheel2", Hash = (VehicleHash)0x4669D038 },

    // Planes

      new VehicleInfo { Name = "alkonost", Hash = (VehicleHash)0xEA313705 },
    new VehicleInfo { Name = "alphaz1", Hash = (VehicleHash)0xA52F6866 },
    new VehicleInfo { Name = "avenger", Hash = (VehicleHash)0x81BD2ED0 },
    new VehicleInfo { Name = "avenger2", Hash = (VehicleHash)0x18606535 },
    new VehicleInfo { Name = "besra", Hash = (VehicleHash)0x6CBD1D6D },
    new VehicleInfo { Name = "BLIMP", Hash = (VehicleHash)0xF7004C86 },
    new VehicleInfo { Name = "BLIMP2", Hash = (VehicleHash)0xDB6B4924 },
    new VehicleInfo { Name = "blimp3", Hash = (VehicleHash)0xEDA4ED97 },
    new VehicleInfo { Name = "bombushka", Hash = (VehicleHash)0xFE0A508C },
    new VehicleInfo { Name = "cargoplane", Hash = (VehicleHash)0x15F27762 },
    new VehicleInfo { Name = "cargoplane2", Hash = (VehicleHash)0x8B4864E1 },
    new VehicleInfo { Name = "cuban800", Hash = (VehicleHash)0xD9927FE3 },
    new VehicleInfo { Name = "dodo", Hash = (VehicleHash)0xCA495705 },
    new VehicleInfo { Name = "duster", Hash = (VehicleHash)0x39D6779E },
    new VehicleInfo { Name = "howard", Hash = (VehicleHash)0xC3F25753 },
    new VehicleInfo { Name = "hydra", Hash = (VehicleHash)0x39D6E83F },
    new VehicleInfo { Name = "jet", Hash = (VehicleHash)0x3F119114 },
    new VehicleInfo { Name = "Lazer", Hash = (VehicleHash)0xB39B0AE6 },
    new VehicleInfo { Name = "luxor", Hash = (VehicleHash)0x250B0C5E },
    new VehicleInfo { Name = "luxor2", Hash = (VehicleHash)0xB79F589E },
    new VehicleInfo { Name = "mammatus", Hash = (VehicleHash)0x97E55D11 },
    new VehicleInfo { Name = "microlight", Hash = (VehicleHash)0x96E24857 },
    new VehicleInfo { Name = "Miljet", Hash = (VehicleHash)0x9D80F93 },
    new VehicleInfo { Name = "mogul", Hash = (VehicleHash)0xD35698EF },
    new VehicleInfo { Name = "molotok", Hash = (VehicleHash)0x5D56F01B },
    new VehicleInfo { Name = "nimbus", Hash = (VehicleHash)0xB2CF7250 },
    new VehicleInfo { Name = "nokota", Hash = (VehicleHash)0x3DC92356 },
    new VehicleInfo { Name = "pyro", Hash = (VehicleHash)0xAD6065C0 },
    new VehicleInfo { Name = "rogue", Hash = (VehicleHash)0xC5DD6967 },
    new VehicleInfo { Name = "seabreeze", Hash = (VehicleHash)0xE8983F9F },
    new VehicleInfo { Name = "Shamal", Hash = (VehicleHash)0xB79C1BF5 },
    new VehicleInfo { Name = "starling", Hash = (VehicleHash)0x9A9EB7DE },
    new VehicleInfo { Name = "strikeforce", Hash = (VehicleHash)0x64DE07A1 },
    new VehicleInfo { Name = "Stunt", Hash = (VehicleHash)0x81794C70 },
    new VehicleInfo { Name = "titan", Hash = (VehicleHash)0x761E2AD3 },
    new VehicleInfo { Name = "tula", Hash = (VehicleHash)0x3E2E4F8A },
    new VehicleInfo { Name = "velum", Hash = (VehicleHash)0x9C429B6A },
    new VehicleInfo { Name = "velum2", Hash = (VehicleHash)0x403820E8 },
    new VehicleInfo { Name = "vestra", Hash = (VehicleHash)0x4FF77E37 },
    new VehicleInfo { Name = "volatol", Hash = (VehicleHash)0x1AAD0DED },

    // Rail

    new VehicleInfo { Name = "cablecar", Hash = (VehicleHash)0xC6C3242D },
    new VehicleInfo { Name = "freight", Hash = (VehicleHash)0x3D6AAA9B },
    new VehicleInfo { Name = "freightcar", Hash = (VehicleHash)0xAFD22A6 },
    new VehicleInfo { Name = "freightcar2", Hash = (VehicleHash)0xBDEC3D99 },
    new VehicleInfo { Name = "freightcont1", Hash = (VehicleHash)0x36DCFF98 },
    new VehicleInfo { Name = "freightcont2", Hash = (VehicleHash)0xE512E79 },
    new VehicleInfo { Name = "freightgrain", Hash = (VehicleHash)0x264D9262 },
    new VehicleInfo { Name = "metrotrain", Hash = (VehicleHash)0x33C9E158 },
    new VehicleInfo { Name = "tankercar", Hash = (VehicleHash)0x22EDDC30 },

    // Sedan 

     new VehicleInfo { Name = "asea", Hash = (VehicleHash)0x94204D89 },
    new VehicleInfo { Name = "asea2", Hash = (VehicleHash)0x9441D8D5 },
    new VehicleInfo { Name = "asterope", Hash = (VehicleHash)0x8E9254FB },
    new VehicleInfo { Name = "cinquemila", Hash = (VehicleHash)0xA4F52C13 },
    new VehicleInfo { Name = "cog55", Hash = (VehicleHash)0x360A438E },
    new VehicleInfo { Name = "cog552", Hash = (VehicleHash)0x29FCD3E4 },
    new VehicleInfo { Name = "cognoscenti", Hash = (VehicleHash)0x86FE0B60 },
    new VehicleInfo { Name = "cognoscenti2", Hash = (VehicleHash)0xDBF2D57A },
    new VehicleInfo { Name = "deity", Hash = (VehicleHash)0x5B531351 },
    new VehicleInfo { Name = "emperor", Hash = (VehicleHash)0xD7278283 },
    new VehicleInfo { Name = "Emperor2", Hash = (VehicleHash)0x8FC3AADC },
    new VehicleInfo { Name = "emperor3", Hash = (VehicleHash)0xB5FCF74E },
    new VehicleInfo { Name = "fugitive", Hash = (VehicleHash)0x71CB2FFB },
    new VehicleInfo { Name = "glendale", Hash = (VehicleHash)0x47A6BC1 },
    new VehicleInfo { Name = "glendale2", Hash = (VehicleHash)0xC98BBAD6 },
    new VehicleInfo { Name = "ingot", Hash = (VehicleHash)0xB3206692 },
    new VehicleInfo { Name = "intruder", Hash = (VehicleHash)0x34DD8AA1 },
    new VehicleInfo { Name = "limo2", Hash = (VehicleHash)0xF92AEC4D },
    new VehicleInfo { Name = "premier", Hash = (VehicleHash)0x8FB66F9B },
    new VehicleInfo { Name = "primo", Hash = (VehicleHash)0xBB6B404F },
    new VehicleInfo { Name = "primo2", Hash = (VehicleHash)0x86618EDA },
    new VehicleInfo { Name = "regina", Hash = (VehicleHash)0xFF22D208 },
    new VehicleInfo { Name = "rhinehart", Hash = (VehicleHash)0x91673D0E },
    new VehicleInfo { Name = "romero", Hash = (VehicleHash)0x2560B2FC },
    new VehicleInfo { Name = "schafter2", Hash = (VehicleHash)0xB52B5113 },
    new VehicleInfo { Name = "schafter5", Hash = (VehicleHash)0xCB0E7CD9 },
    new VehicleInfo { Name = "schafter6", Hash = (VehicleHash)0x72934BE4 },
    new VehicleInfo { Name = "stafford", Hash = (VehicleHash)0x1324E960 },
    new VehicleInfo { Name = "stanier", Hash = (VehicleHash)0xA7EDE74D },
    new VehicleInfo { Name = "stratum", Hash = (VehicleHash)0x66B4FC45 },
    new VehicleInfo { Name = "stretch", Hash = (VehicleHash)0x8B13F083 },
    new VehicleInfo { Name = "superd", Hash = (VehicleHash)0x42F2ED16 },
    new VehicleInfo { Name = "surge", Hash = (VehicleHash)0x8F0E3594 },
    new VehicleInfo { Name = "tailgater", Hash = (VehicleHash)0xC3DDFDCE },
    new VehicleInfo { Name = "tailgater2", Hash = (VehicleHash)0xB5D306A4 },
    new VehicleInfo { Name = "warrener", Hash = (VehicleHash)0x51D83328 },
    new VehicleInfo { Name = "warrener2", Hash = (VehicleHash)0x2290C50A },
    new VehicleInfo { Name = "washington", Hash = (VehicleHash)0x69F06B57 },

    // Service

    new VehicleInfo { Name = "Airbus", Hash = (VehicleHash)0x4C80EB0E },
    new VehicleInfo { Name = "Brickade", Hash = (VehicleHash)0xEDC6F847 },
    new VehicleInfo { Name = "Brickade 6x6", Hash = (VehicleHash)0xA2073353 },
    new VehicleInfo { Name = "Bus", Hash = (VehicleHash)0xD577C962 },
    new VehicleInfo { Name = "Dashound", Hash = (VehicleHash)0x84718D34 },
    new VehicleInfo { Name = "Festival Bus", Hash = (VehicleHash)0x149BD32A },
    new VehicleInfo { Name = "Dune", Hash = (VehicleHash)0x829A3C44 },
    new VehicleInfo { Name = "Rental Shuttle Bus", Hash = (VehicleHash)0xBE819C63 },
    new VehicleInfo { Name = "Taxi", Hash = (VehicleHash)0xC703DB5F },
    new VehicleInfo { Name = "Tourbus", Hash = (VehicleHash)0x73B1C3CB },
    new VehicleInfo { Name = "Trashmaster", Hash = (VehicleHash)0x72435A19 },
    new VehicleInfo { Name = "Trashmaster", Hash = (VehicleHash)0xB527915C },
    new VehicleInfo { Name = "Wastelander", Hash = (VehicleHash)0x8E08EC82 },

    // Sports

    new VehicleInfo { Name = "alpha", Hash = (VehicleHash)0x2DB8D1AA },
    new VehicleInfo { Name = "banshee", Hash = (VehicleHash)0xC1E908D2 },
    new VehicleInfo { Name = "bestiagts", Hash = (VehicleHash)0x4BFCF28B },
    new VehicleInfo { Name = "blista2", Hash = (VehicleHash)0x3DEE5EDA },
    new VehicleInfo { Name = "blista3", Hash = (VehicleHash)0xDCBC1C3B },
    new VehicleInfo { Name = "buffalo", Hash = (VehicleHash)0xEDD516C6 },
    new VehicleInfo { Name = "buffalo2", Hash = (VehicleHash)0x2BEC3CBE },
    new VehicleInfo { Name = "buffalo3", Hash = (VehicleHash)0xE2C013E },
    new VehicleInfo { Name = "calico", Hash = (VehicleHash)0xB8D657AD },
    new VehicleInfo { Name = "carbonizzare", Hash = (VehicleHash)0x7B8AB45F },
    new VehicleInfo { Name = "comet2", Hash = (VehicleHash)0xC1AE4D16 },
    new VehicleInfo { Name = "comet3", Hash = (VehicleHash)0x877358AD },
    new VehicleInfo { Name = "comet4", Hash = (VehicleHash)0x5D1903F9 },
    new VehicleInfo { Name = "comet5", Hash = (VehicleHash)0x276D98A3 },
    new VehicleInfo { Name = "comet6", Hash = (VehicleHash)0x991EFC04 },
    new VehicleInfo { Name = "comet7", Hash = (VehicleHash)0x440851D8 },
    new VehicleInfo { Name = "coquette", Hash = (VehicleHash)0x67BC037 },
    new VehicleInfo { Name = "coquette4", Hash = (VehicleHash)0x98F65A5E },
    new VehicleInfo { Name = "corsita", Hash = (VehicleHash)0xD3046147 },
    new VehicleInfo { Name = "cypher", Hash = (VehicleHash)0x68A5D1EF },
    new VehicleInfo { Name = "drafter", Hash = (VehicleHash)0x28EAB80F },
    new VehicleInfo { Name = "elegy", Hash = (VehicleHash)0xBBA2261 },
    new VehicleInfo { Name = "elegy2", Hash = (VehicleHash)0xDE3D9D22 },
    new VehicleInfo { Name = "Euros", Hash = (VehicleHash)0x7980BDD5 },
    new VehicleInfo { Name = "everon2", Hash = (VehicleHash)0xF82BC92E },
    new VehicleInfo { Name = "feltzer2", Hash = (VehicleHash)0x8911B9F5 },
    new VehicleInfo { Name = "flashgt", Hash = (VehicleHash)0xB4F32118 },
    new VehicleInfo { Name = "furoregt", Hash = (VehicleHash)0xBF1691E0 },
    new VehicleInfo { Name = "fusilade", Hash = (VehicleHash)0x1DC0BA53 },
    new VehicleInfo { Name = "futo", Hash = (VehicleHash)0x7836CE2F },
    new VehicleInfo { Name = "futo2", Hash = (VehicleHash)0xA6297CC8 },
    new VehicleInfo { Name = "gb200", Hash = (VehicleHash)0x71CBEA98 },
    new VehicleInfo { Name = "growler", Hash = (VehicleHash)0x4DC079D7 },
    new VehicleInfo { Name = "hotring", Hash = (VehicleHash)0x42836BE5 },
    new VehicleInfo { Name = "imorgon", Hash = (VehicleHash)0xBC7C0A00 },
    new VehicleInfo { Name = "issi7", Hash = (VehicleHash)0x6E8DA4F7 },
    new VehicleInfo { Name = "italigto", Hash = (VehicleHash)0xEC3E3404 },
    new VehicleInfo { Name = "italirsx", Hash = (VehicleHash)0xBB78956A },
    new VehicleInfo { Name = "jester", Hash = (VehicleHash)0xB2A716A3 },
    new VehicleInfo { Name = "jester2", Hash = (VehicleHash)0xBE0E6126 },
    new VehicleInfo { Name = "jester3", Hash = (VehicleHash)0xF330CB6A },
    new VehicleInfo { Name = "jester4", Hash = (VehicleHash)0xA1B3A871 },
    new VehicleInfo { Name = "jugular", Hash = (VehicleHash)0xF38C4245 },
    new VehicleInfo { Name = "khamelion", Hash = (VehicleHash)0x206D1B68 },
    new VehicleInfo { Name = "komoda", Hash = (VehicleHash)0xCE44C4B9 },
    new VehicleInfo { Name = "kuruma", Hash = (VehicleHash)0xAE2BFE94 },
    new VehicleInfo { Name = "kuruma2", Hash = (VehicleHash)0x187D938D },
    new VehicleInfo { Name = "locust", Hash = (VehicleHash)0xC7E55211 },
    new VehicleInfo { Name = "lynx", Hash = (VehicleHash)0x1CBDC10B },
    new VehicleInfo { Name = "massacro", Hash = (VehicleHash)0xF77ADE32 },
    new VehicleInfo { Name = "massacro2", Hash = (VehicleHash)0xDA5819A3 },
    new VehicleInfo { Name = "neo", Hash = (VehicleHash)0x9F6ED5A2 },
    new VehicleInfo { Name = "neon", Hash = (VehicleHash)0x91CA96EE },
    new VehicleInfo { Name = "ninef", Hash = (VehicleHash)0x3D8FA25C },
    new VehicleInfo { Name = "ninef2", Hash = (VehicleHash)0xA8E38B01 },
    new VehicleInfo { Name = "omnis", Hash = (VehicleHash)0xD1AD4937 },
    new VehicleInfo { Name = "omnisegt", Hash = (VehicleHash)0xE1E2E6D7 },
    new VehicleInfo { Name = "panthere", Hash = (VehicleHash)0x7D326F04 },
    new VehicleInfo { Name = "paragon", Hash = (VehicleHash)0xE550775B },
    new VehicleInfo { Name = "paragon2", Hash = (VehicleHash)0x546D8EEE },
    new VehicleInfo { Name = "pariah", Hash = (VehicleHash)0x33B98FE2 },
    new VehicleInfo { Name = "penumbra", Hash = (VehicleHash)0xE9805550 },
    new VehicleInfo { Name = "penumbra2", Hash = (VehicleHash)0xDA5EC7DA },
    new VehicleInfo { Name = "r300", Hash = (VehicleHash)0x402586F8 },
    new VehicleInfo { Name = "raiden", Hash = (VehicleHash)0xA4D99B7D },
    new VehicleInfo { Name = "RapidGT", Hash = (VehicleHash)0x8CB29A14 },
    new VehicleInfo { Name = "RapidGT2", Hash = (VehicleHash)0x679450AF },
    new VehicleInfo { Name = "raptor", Hash = (VehicleHash)0xD7C56D39 },
    new VehicleInfo { Name = "remus", Hash = (VehicleHash)0x5216AD5E },
    new VehicleInfo { Name = "revolter", Hash = (VehicleHash)0xE78CC3D9 },
    new VehicleInfo { Name = "rt3000", Hash = (VehicleHash)0xE505CF99 },
    new VehicleInfo { Name = "ruston", Hash = (VehicleHash)0x2AE524A8 },
    new VehicleInfo { Name = "schafter3", Hash = (VehicleHash)0xA774B5A6 },
    new VehicleInfo { Name = "schafter4", Hash = (VehicleHash)0x58CF185C },
    new VehicleInfo { Name = "schlagen", Hash = (VehicleHash)0xE1C03AB0 },
    new VehicleInfo { Name = "schwarzer", Hash = (VehicleHash)0xD37B7976 },
    new VehicleInfo { Name = "sentinel3", Hash = (VehicleHash)0x41D149AA },
    new VehicleInfo { Name = "sentinel4", Hash = (VehicleHash)0xAF1FA439 },
    new VehicleInfo { Name = "SEVEN70", Hash = (VehicleHash)0x97398A4B },
    new VehicleInfo { Name = "sm722", Hash = (VehicleHash)0x2E3967B0 },
    new VehicleInfo { Name = "SPECTER", Hash = (VehicleHash)0x706E2B40 },
    new VehicleInfo { Name = "SPECTER2", Hash = (VehicleHash)0x400F5147 },
    new VehicleInfo { Name = "streiter", Hash = (VehicleHash)0x67D2B389 },
    new VehicleInfo { Name = "Sugoi", Hash = (VehicleHash)0x3ADB9758 },
    new VehicleInfo { Name = "sultan", Hash = (VehicleHash)0x39DA2754 },
    new VehicleInfo { Name = "sultan2", Hash = (VehicleHash)0x3404691C },
    new VehicleInfo { Name = "sultan3", Hash = (VehicleHash)0xEEA75E63 },
    new VehicleInfo { Name = "Surano", Hash = (VehicleHash)0x16E478C1 },
    new VehicleInfo { Name = "tampa2", Hash = (VehicleHash)0xC0240885 },
    new VehicleInfo { Name = "tenf", Hash = (VehicleHash)0xCAB6E261 },
    new VehicleInfo { Name = "tenf2", Hash = (VehicleHash)0x10635A0E },
    new VehicleInfo { Name = "tropos", Hash = (VehicleHash)0x707E63A4 },
    new VehicleInfo { Name = "vectre", Hash = (VehicleHash)0xA42FC3A5 },
    new VehicleInfo { Name = "verlierer2", Hash = (VehicleHash)0x41B77FA4 },
    new VehicleInfo { Name = "veto", Hash = (VehicleHash)0xCCE5C8FA },
    new VehicleInfo { Name = "veto2", Hash = (VehicleHash)0xA703E4A9 },
    new VehicleInfo { Name = "vstr", Hash = (VehicleHash)0x56CDEE7D },
    new VehicleInfo { Name = "zr350", Hash = (VehicleHash)0x91373058 },
    new VehicleInfo { Name = "zr380", Hash = (VehicleHash)0x20314B42 },
    new VehicleInfo { Name = "zr3802", Hash = (VehicleHash)0xBE11EFC6 },
    new VehicleInfo { Name = "zr3803", Hash = (VehicleHash)0xA7DCC35C },

    // Sports Classic

    new VehicleInfo { Name = "Ardent", Hash = (VehicleHash)0x97E5533 },
new VehicleInfo { Name = "Roosevelt", Hash = (VehicleHash)0x6FF6914 },
new VehicleInfo { Name = "Fränken Stange", Hash = (VehicleHash)0xCE6B35A4 },
new VehicleInfo { Name = "Roosevelt Valor", Hash = (VehicleHash)0xDC19D101 },
new VehicleInfo { Name = "Casco", Hash = (VehicleHash)0x3822BDFE },
new VehicleInfo { Name = "Cheburek", Hash = (VehicleHash)0xC514AAE0 },
new VehicleInfo { Name = "Cheetah Classic", Hash = (VehicleHash)0xD4E5F4D },
new VehicleInfo { Name = "Coquette Classic", Hash = (VehicleHash)0x3C4E2113 },
new VehicleInfo { Name = "Deluxo", Hash = (VehicleHash)0x586765FB },
new VehicleInfo { Name = "Dynasty", Hash = (VehicleHash)0x127E90D5 },
new VehicleInfo { Name = "Fagaloa", Hash = (VehicleHash)0x6068AD86 },
new VehicleInfo { Name = "Stirling GT", Hash = (VehicleHash)0xA29D6D10 },
new VehicleInfo { Name = "GT500", Hash = (VehicleHash)0x8408F33A },
new VehicleInfo { Name = "Infernus Classic", Hash = (VehicleHash)0xAC33179C },
new VehicleInfo { Name = "JB 700", Hash = (VehicleHash)0x3EAB5555 },
new VehicleInfo { Name = "JB 700W", Hash = (VehicleHash)0x177DA45C },
new VehicleInfo { Name = "Mamba", Hash = (VehicleHash)0x9CFFFC56 },
new VehicleInfo { Name = "Manana", Hash = (VehicleHash)0x81634188 },
new VehicleInfo { Name = "Michelli GT", Hash = (VehicleHash)0x3E5BD8D9 },
new VehicleInfo { Name = "Monroe", Hash = (VehicleHash)0xE62B361B },
new VehicleInfo { Name = "Nebula Turbo", Hash = (VehicleHash)0xCB642637 },
new VehicleInfo { Name = "Peyote", Hash = (VehicleHash)0x6D19CCBC },
new VehicleInfo { Name = "Peyote Custom", Hash = (VehicleHash)0x4201A843 },
new VehicleInfo { Name = "Pigalle", Hash = (VehicleHash)0x404B6381 },
new VehicleInfo { Name = "Rapid GT Classic", Hash = (VehicleHash)0x7A2EF5E4 },
new VehicleInfo { Name = "Retinue", Hash = (VehicleHash)0x6DBD6C0A },
new VehicleInfo { Name = "Retinue Mk II", Hash = (VehicleHash)0x79178F0A },
new VehicleInfo { Name = "Savestra", Hash = (VehicleHash)0x35DED0DD },
new VehicleInfo { Name = "Stinger", Hash = (VehicleHash)0x5C23AF9B },
new VehicleInfo { Name = "Stinger GT", Hash = (VehicleHash)0x82E499FA },
new VehicleInfo { Name = "Stromberg", Hash = (VehicleHash)0x34DBA661 },
new VehicleInfo { Name = "Swinger", Hash = (VehicleHash)0x1DD4C0FF },
new VehicleInfo { Name = "Toreador", Hash = (VehicleHash)0x56C8A5EF },
new VehicleInfo { Name = "Torero", Hash = (VehicleHash)0x59A9E570 },
new VehicleInfo { Name = "Tornado", Hash = (VehicleHash)0x1BB290BC },
new VehicleInfo { Name = "Tornado", Hash = (VehicleHash)0x5B42A5C4 },
new VehicleInfo { Name = "Tornado", Hash = (VehicleHash)0x690A4153 },
new VehicleInfo { Name = "Tornado", Hash = (VehicleHash)0x86CF7CDD },
new VehicleInfo { Name = "Tornado Custom", Hash = (VehicleHash)0x94DA98EF },
new VehicleInfo { Name = "Tornado Rat Rod", Hash = (VehicleHash)0xA31CB573 },
new VehicleInfo { Name = "Turismo Classic", Hash = (VehicleHash)0xC575DF11 },
new VehicleInfo { Name = "Viseris", Hash = (VehicleHash)0xE8A8BA94 },
new VehicleInfo { Name = "190z", Hash = (VehicleHash)0x3201DD49 },
new VehicleInfo { Name = "Zion Classic", Hash = (VehicleHash)0x6F039A67 },
new VehicleInfo { Name = "Z-Type", Hash = (VehicleHash)0x2D3BD401 },

         
// Super

new VehicleInfo { Name = "Adder", Hash = (VehicleHash)0xB779A091 },
new VehicleInfo { Name = "Autarch", Hash = (VehicleHash)0xED552C74 },
new VehicleInfo { Name = "Banshee 900R", Hash = (VehicleHash)0x25C5AF13 },
new VehicleInfo { Name = "Bullet", Hash = (VehicleHash)0x9AE6DDA1 },
new VehicleInfo { Name = "Champion", Hash = (VehicleHash)0xC972A155 },
new VehicleInfo { Name = "Cheetah", Hash = (VehicleHash)0xB1D95DA0 },
new VehicleInfo { Name = "Cyclone", Hash = (VehicleHash)0x52FF9437 },
new VehicleInfo { Name = "Deveste Eight", Hash = (VehicleHash)0x5EE005DA },
new VehicleInfo { Name = "Emerus", Hash = (VehicleHash)0x4EE74355 },
new VehicleInfo { Name = "Entity XXR", Hash = (VehicleHash)0x8198AEDC },
new VehicleInfo { Name = "Entity MT", Hash = (VehicleHash)0x6838FC1D },
new VehicleInfo { Name = "Entity XF", Hash = (VehicleHash)0xB2FE5CF9 },
new VehicleInfo { Name = "FMJ", Hash = (VehicleHash)0x5502626C },
new VehicleInfo { Name = "Furia", Hash = (VehicleHash)0x3944D5A0 },
new VehicleInfo { Name = "GP1", Hash = (VehicleHash)0x4992196C },
new VehicleInfo { Name = "Ignus", Hash = (VehicleHash)0xA9EC907B },
new VehicleInfo { Name = "Infernus", Hash = (VehicleHash)0x18F25AC7 },
new VehicleInfo { Name = "Itali GTB", Hash = (VehicleHash)0x85E8E76B },
new VehicleInfo { Name = "Itali GTB Custom", Hash = (VehicleHash)0xE33A477B },
new VehicleInfo { Name = "Krieger", Hash = (VehicleHash)0xD86A0247 },
new VehicleInfo { Name = "RE-7B", Hash = (VehicleHash)0xB6846A55 },
new VehicleInfo { Name = "LM87", Hash = (VehicleHash)0xFF5968CD },
new VehicleInfo { Name = "Nero", Hash = (VehicleHash)0x3DA47243 },
new VehicleInfo { Name = "Nero Custom", Hash = (VehicleHash)0x4131F378 },
new VehicleInfo { Name = "Osiris", Hash = (VehicleHash)0x767164D6 },
new VehicleInfo { Name = "Penetrator", Hash = (VehicleHash)0x9734F3EA },
new VehicleInfo { Name = "811", Hash = (VehicleHash)0x92EF6E04 },
new VehicleInfo { Name = "X80 Proto", Hash = (VehicleHash)0x7E8F677F },
new VehicleInfo { Name = "Reaper", Hash = (VehicleHash)0xDF381E5 },
new VehicleInfo { Name = "S80RR", Hash = (VehicleHash)0xECA6B6A3 },
new VehicleInfo { Name = "SC1", Hash = (VehicleHash)0x5097F589 },
new VehicleInfo { Name = "Scramjet", Hash = (VehicleHash)0xD9F0503D },
new VehicleInfo { Name = "ETR1", Hash = (VehicleHash)0x30D3F6D8 },
new VehicleInfo { Name = "Sultan RS", Hash = (VehicleHash)0xEE6024BC },
new VehicleInfo { Name = "T20", Hash = (VehicleHash)0x6322B39A },
new VehicleInfo { Name = "Taipan", Hash = (VehicleHash)0xBC5DC07E },
new VehicleInfo { Name = "Tempesta", Hash = (VehicleHash)0x1044926F },
new VehicleInfo { Name = "Tezeract", Hash = (VehicleHash)0x3D7C6410 },
new VehicleInfo { Name = "Thrax", Hash = (VehicleHash)0x3E3D1F59 },
new VehicleInfo { Name = "Tigon", Hash = (VehicleHash)0xAF0B8D48 },
new VehicleInfo { Name = "Torero XO", Hash = (VehicleHash)0xF62446BA },
new VehicleInfo { Name = "Turismo R", Hash = (VehicleHash)0x185484E1 },
new VehicleInfo { Name = "Tyrant", Hash = (VehicleHash)0xE99011C2 },
new VehicleInfo { Name = "Tyrus", Hash = (VehicleHash)0x7B406EFB },
new VehicleInfo { Name = "Vacca", Hash = (VehicleHash)0x142E0DC3 },
new VehicleInfo { Name = "Vagner", Hash = (VehicleHash)0x7397224C },
new VehicleInfo { Name = "Vigilante", Hash = (VehicleHash)0xB5EF4C33 },
new VehicleInfo { Name = "Virtue", Hash = (VehicleHash)0x27E34161 },
new VehicleInfo { Name = "Visione", Hash = (VehicleHash)0xC4810400 },
new VehicleInfo { Name = "Voltic", Hash = (VehicleHash)0x9F4B77BE },
new VehicleInfo { Name = "Rocket Voltic", Hash = (VehicleHash)0x3AF76F4A },
new VehicleInfo { Name = "XA-21", Hash = (VehicleHash)0x36B4A8A9 },
new VehicleInfo { Name = "Zeno", Hash = (VehicleHash)0x2714AA93 },
new VehicleInfo { Name = "Zentorno", Hash = (VehicleHash)0xAC5DF515 },
new VehicleInfo { Name = "Zorrusso", Hash = (VehicleHash)0xD757D97D },

// Suv

new VehicleInfo { Name = "Astron", Hash = (VehicleHash)0x258C9364 },
new VehicleInfo { Name = "Baller", Hash = (VehicleHash)0xCFCA3668 },
new VehicleInfo { Name = "Baller", Hash = (VehicleHash)0x8852855 },
new VehicleInfo { Name = "Baller LE", Hash = (VehicleHash)0x6FF0F727 },
new VehicleInfo { Name = "Baller LE LWB", Hash = (VehicleHash)0x25CBE2E2 },
new VehicleInfo { Name = "Baller LE (Armored)", Hash = (VehicleHash)0x1C09CF5E },
new VehicleInfo { Name = "Baller LE LWB (Armored)", Hash = (VehicleHash)0x27B4E6B0 },
new VehicleInfo { Name = "Baller ST", Hash = (VehicleHash)0x1573422D },
new VehicleInfo { Name = "BjXL", Hash = (VehicleHash)0x32B29A4B },
new VehicleInfo { Name = "Cavalcade", Hash = (VehicleHash)0x779F23AA },
new VehicleInfo { Name = "Cavalcade", Hash = (VehicleHash)0xD0EB2BE5 },
new VehicleInfo { Name = "Contender", Hash = (VehicleHash)0x28B67ACA },
new VehicleInfo { Name = "Dubsta", Hash = (VehicleHash)0x462FE277 },
new VehicleInfo { Name = "Dubsta", Hash = (VehicleHash)0xE882E5F6 },
new VehicleInfo { Name = "FQ 2", Hash = (VehicleHash)0xBC32A33B },
new VehicleInfo { Name = "Granger", Hash = (VehicleHash)0x9628879C },
new VehicleInfo { Name = "Granger 3600LX", Hash = (VehicleHash)0xF06C29C7 },
new VehicleInfo { Name = "Gresley", Hash = (VehicleHash)0xA3FC0F4D },
new VehicleInfo { Name = "Habanero", Hash = (VehicleHash)0x34B7390F },
new VehicleInfo { Name = "Huntley S", Hash = (VehicleHash)0x1D06D681 },
new VehicleInfo { Name = "Issi Rally", Hash = (VehicleHash)0x5C6C00B4 },
new VehicleInfo { Name = "I-Wagen", Hash = (VehicleHash)0x27816B7E },
new VehicleInfo { Name = "Jubilee", Hash = (VehicleHash)0x1B8165D3 },
new VehicleInfo { Name = "Landstalker", Hash = (VehicleHash)0x4BA4E8DC },
new VehicleInfo { Name = "Landstalker XL", Hash = (VehicleHash)0xCE0B9F22 },
new VehicleInfo { Name = "Mesa", Hash = (VehicleHash)0x36848602 },
new VehicleInfo { Name = "Mesa", Hash = (VehicleHash)0xD36A4B44 },
new VehicleInfo { Name = "Novak", Hash = (VehicleHash)0x92F5024E },
new VehicleInfo { Name = "Patriot", Hash = (VehicleHash)0xCFCFEB3B },
new VehicleInfo { Name = "Patriot Stretch", Hash = (VehicleHash)0xE6E967F8 },
new VehicleInfo { Name = "Radius", Hash = (VehicleHash)0x9D96B45B },
new VehicleInfo { Name = "Rebla GTS", Hash = (VehicleHash)0x4F48FC4 },
new VehicleInfo { Name = "Rocoto", Hash = (VehicleHash)0x7F5C91F1 },
new VehicleInfo { Name = "Seminole", Hash = (VehicleHash)0x48CECED3 },
new VehicleInfo { Name = "Seminole Frontier", Hash = (VehicleHash)0x94114926 },
new VehicleInfo { Name = "Serrano", Hash = (VehicleHash)0x4FB1A214 },
new VehicleInfo { Name = "Squaddie", Hash = (VehicleHash)0xF9E67C05 },
new VehicleInfo { Name = "Toros", Hash = (VehicleHash)0xBA5334AC },
new VehicleInfo { Name = "XLS", Hash = (VehicleHash)0x47BBCF2E },
new VehicleInfo { Name = "XLS (Armored)", Hash = (VehicleHash)0xE6401328 },

// Utility 

new VehicleInfo { Name = "Airtug", Hash = (VehicleHash)0x5D0AAC8F },
new VehicleInfo { Name = "Army Trailer", Hash = (VehicleHash)0xB8081009 },
new VehicleInfo { Name = "Army Trailer", Hash = (VehicleHash)0xA7FF33F5 },
new VehicleInfo { Name = "Army Trailer", Hash = (VehicleHash)0x9E6B14D6 },
new VehicleInfo { Name = "Baletrailer", Hash = (VehicleHash)0xE82AE656 },
new VehicleInfo { Name = "Boat Trailer", Hash = (VehicleHash)0x1F3D44B5 },
new VehicleInfo { Name = "Caddy", Hash = (VehicleHash)0x44623884 },
new VehicleInfo { Name = "Caddy", Hash = (VehicleHash)0xDFF0594C },
new VehicleInfo { Name = "Caddy", Hash = (VehicleHash)0xD227BDBB },
new VehicleInfo { Name = "Docktug", Hash = (VehicleHash)0xCB44B1CA },
new VehicleInfo { Name = "Forklift", Hash = (VehicleHash)0x58E49664 },
new VehicleInfo { Name = "Graintrailer", Hash = (VehicleHash)0x3CC7F596 },
new VehicleInfo { Name = "Lawn Mower", Hash = (VehicleHash)0x6A4BD8F6 },
new VehicleInfo { Name = "Ripley", Hash = (VehicleHash)0xCD935EF9 },
new VehicleInfo { Name = "Sadler", Hash = (VehicleHash)0xDC434E51 },
new VehicleInfo { Name = "Sadler", Hash = (VehicleHash)0x2BC345D1 },
new VehicleInfo { Name = "Scrap Truck", Hash = (VehicleHash)0x9A9FD3DF },
new VehicleInfo { Name = "Slamtruck", Hash = (VehicleHash)0xC1A8A914 },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0xD1ABB666 },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x74998082 },
new VehicleInfo { Name = "Towtruck", Hash = (VehicleHash)0xB12314E0 },
new VehicleInfo { Name = "Towtruck", Hash = (VehicleHash)0xE5A2D6C6 },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x7BE032C6 },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x6A59902D },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x7CAB34D0 },
new VehicleInfo { Name = "Tractor", Hash = (VehicleHash)0x61D6BA8C },
new VehicleInfo { Name = "Fieldmaster", Hash = (VehicleHash)0x843B73DE },
new VehicleInfo { Name = "Fieldmaster", Hash = (VehicleHash)0x562A97BD },
new VehicleInfo { Name = "Mobile Operations Center", Hash = (VehicleHash)0x5993F939 },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x782A236D },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0xCBB2BE0E },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0xA1DA3C91 },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x8548036D },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0xBE66F5AA },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x2A72BEAB },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0xAF62F6B2 },
new VehicleInfo { Name = "Trailer", Hash = (VehicleHash)0x967620BE },
new VehicleInfo { Name = "Utility Truck", Hash = (VehicleHash)0x1ED0A534 },
new VehicleInfo { Name = "Utility Truck", Hash = (VehicleHash)0x34E6BF6B },
new VehicleInfo { Name = "Utility Truck", Hash = (VehicleHash)0x7F2153DF },

// Vans

new VehicleInfo { Name = "Bison", Hash = (VehicleHash)0xFEFD644F },
new VehicleInfo { Name = "Bison", Hash = (VehicleHash)0x7B8297C5 },
new VehicleInfo { Name = "Bison", Hash = (VehicleHash)0x67B3F020 },
new VehicleInfo { Name = "Bobcat XL", Hash = (VehicleHash)0x3FC5D440 },
new VehicleInfo { Name = "Boxville", Hash = (VehicleHash)0x898ECCEA },
new VehicleInfo { Name = "Boxville", Hash = (VehicleHash)0xF21B33BE },
new VehicleInfo { Name = "Boxville", Hash = (VehicleHash)0x7405E08 },
new VehicleInfo { Name = "Boxville", Hash = (VehicleHash)0x1A79847A },
new VehicleInfo { Name = "Boxville", Hash = (VehicleHash)0x28AD20E1 },
new VehicleInfo { Name = "Burrito", Hash = (VehicleHash)0xAFBB2CA4 },
new VehicleInfo { Name = "Burrito", Hash = (VehicleHash)0xC9E8FF76 },
new VehicleInfo { Name = "Burrito", Hash = (VehicleHash)0x98171BD3 },
new VehicleInfo { Name = "Burrito", Hash = (VehicleHash)0x353B561D },
new VehicleInfo { Name = "Burrito", Hash = (VehicleHash)0x437CF2A0 },
new VehicleInfo { Name = "Camper", Hash = (VehicleHash)0x6FD95F68 },
new VehicleInfo { Name = "Gang Burrito", Hash = (VehicleHash)0x97FA4F36 },
new VehicleInfo { Name = "Gang Burrito", Hash = (VehicleHash)0x11AA0E14 },
new VehicleInfo { Name = "Journey", Hash = (VehicleHash)0xF8D48E7A },
new VehicleInfo { Name = "Journey II", Hash = (VehicleHash)0x9F04C481 },
new VehicleInfo { Name = "Minivan", Hash = (VehicleHash)0xED7EADA4 },
new VehicleInfo { Name = "Minivan Custom", Hash = (VehicleHash)0xBCDE91F0 },
new VehicleInfo { Name = "Paradise", Hash = (VehicleHash)0x58B3979C },
new VehicleInfo { Name = "Pony", Hash = (VehicleHash)0xF8DE29A8 },
new VehicleInfo { Name = "Pony", Hash = (VehicleHash)0x38408341 },
new VehicleInfo { Name = "Rumpo", Hash = (VehicleHash)0x4543B74D },
new VehicleInfo { Name = "Rumpo", Hash = (VehicleHash)0x961AFEF7 },
new VehicleInfo { Name = "Rumpo", Hash = (VehicleHash)0x57F682AF },
new VehicleInfo { Name = "Speedo", Hash = (VehicleHash)0xCFB3870C },
new VehicleInfo { Name = "Speedo", Hash = (VehicleHash)0x2B6DC64A },
new VehicleInfo { Name = "Speedo", Hash = (VehicleHash)0xD17099D },
new VehicleInfo { Name = "Surfer", Hash = (VehicleHash)0x29B0DA97 },
new VehicleInfo { Name = "Surfer", Hash = (VehicleHash)0xB1D80E06 },
new VehicleInfo { Name = "Surfer Custom", Hash = (VehicleHash)0xC247AEE5 },
new VehicleInfo { Name = "Taco Van", Hash = (VehicleHash)0x744CA80D },
new VehicleInfo { Name = "Youga", Hash = (VehicleHash)0x3E5F6B8 },
new VehicleInfo { Name = "Youga Classic", Hash = (VehicleHash)0x3D29CD2B },
new VehicleInfo { Name = "Youga Classic 4x4", Hash = (VehicleHash)0x6B73A9BE },
new VehicleInfo { Name = "Youga Custom", Hash = (VehicleHash)0x589A840C },

// Others

new VehicleInfo { Name = "Arbiter GT", Hash = (VehicleHash)0x5C54030C },
new VehicleInfo { Name = "Astron Custom", Hash = (VehicleHash)0xA71D0D4F },
new VehicleInfo { Name = "Cyclone II", Hash = (VehicleHash)0x170341C2 },
new VehicleInfo { Name = "Weaponized Ignus", Hash = (VehicleHash)0x39085F47 },
new VehicleInfo { Name = "S95", Hash = (VehicleHash)0x438F6593 },








};


    private void SpawnVehicle(string vehicleName)
    {
    
        VehicleInfo selectedVehicle = VehicleList.Find(v => v.Name.Equals(vehicleName, StringComparison.OrdinalIgnoreCase));

        if (selectedVehicle != null)
        {
           
            Notification.Show($"Spawning {selectedVehicle.Name}");

          
            Vector3 spawnPosition = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5.0f; 

            
            Vehicle vehicle = World.CreateVehicle(selectedVehicle.Hash, spawnPosition);

            if (vehicle != null)
            {
               
                Game.Player.Character.SetIntoVehicle(vehicle, VehicleSeat.Driver);


                Notification.Show($"~g~Successfully~s~ Created {selectedVehicle.Name}");

            }
            else
            {
               
                Notification.Show("~r~Failed~s~ To Create Vehicle");
            }
        }

        { 
           
        }
    }

    public class VehicleInfo
    {
        public string Name { get; set; }
        public VehicleHash Hash { get; set; }
    }







    // Teleport















    // Recovery


  










    // Money




    private void AddRemoveMoneyMenuItem(NativeMenu submenu, string title, int amount)
    {
        NativeItem nativeItem = new NativeItem(title);
        nativeItem.Activated += (sender, e) =>
        {
            try
            {
                if (amount > 0)
                {
                    AddMoney(amount);
                }
                else if (amount < 0)
                {
                    RemoveMoney(Math.Abs(amount));
                }
                else
                {
                    throw new ArgumentException("Amount should be non-zero for money operations.");
                }
            }
            catch (Exception ex)
            {
                Notification.Show($"~r~Error:~s~ {ex.Message}");
            }
        };
        submenu.Add(nativeItem);
    }

    private void AddMoney(int amount)
    {
        Game.Player.Money += amount;
        Notification.Show(string.Format("Added ~g~${0:N0}~s~ To Your Account", (object)amount));
    }

    private void RemoveMoney(int amount)
    {
        int money = Game.Player.Money;
        if (money >= amount)
        {
            Game.Player.Money = money - amount;
            Notification.Show(string.Format("Removed ~r~${0:N0}~s~ From Your Account", (object)amount));
        }
        else
        {
            throw new InvalidOperationException("You don't have enough money to perform this operation.");
        }
    }

    private void RemoveAllMoney()
    {
        Game.Player.Money = 0;
        Notification.Show("Bank Account Cleared");
    }

    private void AddClearBankMenuItem(NativeMenu submenu, string title)
    {
        NativeItem nativeItem = new NativeItem(title);
        nativeItem.Activated += (sender, e) =>
        {
            try
            {
                ClearBank();
            }
            catch (Exception ex)
            {
                Notification.Show($"~r~Error: {ex.Message}");
            }
        };
        submenu.Add(nativeItem);
    }


    private void ClearBank()
    {
        int currentMoney = Game.Player.Money;

        if (currentMoney > 0)
        {
        
            Game.Player.Money = 0;

         
            Notification.Show(string.Format("Removed ~r~${0:N0}~s~ From Your Account", (object)currentMoney));
        }
        else
        {
            throw new InvalidOperationException("Bank Already Empty");
        }
    }




    // Time
    private void SetTime(int hours, int minutes)
    {
        Function.Call(Hash.NETWORK_OVERRIDE_CLOCK_TIME, hours, minutes, 0);

        string amPm = (hours >= 12) ? "PM" : "AM";
        int displayHours = (hours > 12) ? hours - 12 : (hours == 0) ? 12 : hours;

        Notification.Show($"Time set to {displayHours:D2}:{minutes:D2} {amPm}");

    }



    // Weather

    private void AddWeatherMenuItem(string weatherType, string displayName, string description, uint weatherHash)
    {
        NativeItem nativeItem = new NativeItem(displayName, description);
        nativeItem.Activated += (sender, e) => SetWeatherState(weatherHash);
        weatherSubMenu.Add(nativeItem);
    }

    private void SetWeatherState(uint weatherHash) => Function.Call(Hash._SET_WEATHER_TYPE_TRANSITION, weatherHash);



    // Misc

    private void CloseGta()
    {
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }




    // Settings


    private void DisableMouseControlsToggle_CheckboxChanged(object sender, EventArgs e)
    {
        if (!(sender is NativeCheckboxItem nativeCheckboxItem))
            return;
        ToggleMouseControls(!nativeCheckboxItem.Checked);
    }

  











    private void ToggleMouseControls(bool enable)
    {
        mainMenu.UseMouse = enable;
        localSubMenu.UseMouse = enable;
        vehiclesSubMenu.UseMouse = enable;
        weaponsSubMenu.UseMouse = enable;
        spawnerSubMenu.UseMouse = enable;
        spawn_vehicleSubMenu.UseMouse = enable;
        boatsSubMenu.UseMouse = enable;
        commercialSubMenu.UseMouse = enable;
        compactSubMenu.UseMouse = enable;
        coupeSubMenu.UseMouse = enable;
        cycleSubMenu.UseMouse = enable;
        emergencySubMenu.UseMouse = enable;
        helicopterSubMenu.UseMouse = enable;
        industrialSubMenu.UseMouse = enable;
        militarySubMenu.UseMouse = enable;
        motorcycleSubMenu.UseMouse = enable;
        muscleSubMenu.UseMouse = enable;
        offRoadSubMenu.UseMouse = enable;
        openWheelSubMenu.UseMouse = enable;
        planeSubMenu.UseMouse = enable;
        railSubMenu.UseMouse = enable;
        sedanSubMenu.UseMouse = enable;
        serviceSubMenu.UseMouse = enable;
        sportsSubMenu.UseMouse = enable;
        sportsClassicSubMenu.UseMouse = enable;
        superSubMenu.UseMouse = enable;
        suvSubMenu.UseMouse = enable;
        utilitySubMenu.UseMouse = enable;
        vanSubMenu.UseMouse = enable;
        othersSubMenu.UseMouse = enable;
        teleportSubMenu.UseMouse = enable;
        recoverySubMenu.UseMouse = enable;
        moneySubMenu.UseMouse = enable;
        addSubMenu.UseMouse = enable;
        removeSubMenu.UseMouse = enable;
        recoverySubMenu.UseMouse = enable;
        worldSubMenu.UseMouse = enable;
        timeSubMenu.UseMouse = enable;
        weatherSubMenu.UseMouse = enable;
        miscSubMenu.UseMouse = enable;
        settingsSubMenu.UseMouse = enable;
        disableSubMenu.UseMouse = enable;
    }

    private void RemoveHelpTextToggle_CheckboxChanged(object sender, EventArgs e)
    {
        if (!(sender is NativeCheckboxItem nativeCheckboxItem))
            return;
        if (nativeCheckboxItem.Checked)
            Tick += RemoveHelpTextOnTick;
        else
            Tick -= RemoveHelpTextOnTick;
    }

    private void RemoveHelpTextOnTick(object sender, EventArgs e) => Function.Call(Hash.CLEAR_ALL_HELP_MESSAGES);

}
        
