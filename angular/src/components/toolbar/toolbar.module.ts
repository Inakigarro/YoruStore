import { NgModule } from "@angular/core";
import { ToolbarFeatureModule } from "./feature/toolbar-feature.module";

@NgModule({
    imports: [
        ToolbarFeatureModule
    ],
    exports: [ToolbarFeatureModule]
})
export class AppToolbarModule {}