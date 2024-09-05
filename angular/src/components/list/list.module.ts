import { NgModule } from "@angular/core";
import { ListFeatureModule } from "./feature/list-feature.module";

@NgModule({
    imports: [ListFeatureModule],
    exports: [ListFeatureModule]
})
export class AppListModule {}