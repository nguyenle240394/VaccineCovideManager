﻿using System.Threading.Tasks;
using VaccineCovidManager.Localization;
using VaccineCovidManager.MultiTenancy;
using VaccineCovidManager.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace VaccineCovidManager.Web.Menus;

public class VaccineCovidManagerMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<VaccineCovidManagerResource>();

        /*context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                VaccineCovidManagerMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );*/

        if (await context.IsGrantedAsync(VaccineCovidManagerPermissions.NoiSanXuats.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCoidManager.NoiSanXuats",
                        l["Menu:NoiSanXuats"],
                        url: "/NoiSanXuats")
            );
        }
        if (await context.IsGrantedAsync(VaccineCovidManagerPermissions.DonViYTes.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCoidManager.DonViYTes",
                        l["Menu:DonViYTes"],
                        url: "/DonViYTes")
            );
        }
        if (await context.IsGrantedAsync(VaccineCovidManagerPermissions.ChiTietNhaps.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCoidManager.ChiTietNhaps",
                        l["Menu:ChiTietNhaps"],
                        url: "/ChiTietNhaps")
            );
        }
        if (await context.IsGrantedAsync(VaccineCovidManagerPermissions.ChiTietXuats.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCoidManager.ChiTietXuats",
                        l["Menu:ChiTietXuats"],
                        url: "/ChiTietXuats")
            );
        }
        if (await context.IsGrantedAsync(VaccineCovidManagerPermissions.VaccineCovids.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCoidManager.VaccineCovids",
                        l["Menu:VaccineCovids"],
                        url: "/VaccineCovids")
            );
        }

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
    }
}
