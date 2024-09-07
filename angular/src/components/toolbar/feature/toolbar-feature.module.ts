import { NgModule } from "@angular/core";
import { ToolbarFeatureComponent } from "./toolbar-feature.component";
import { CommonModule } from "@angular/common";
import { ToolbarUiModule } from "../ui/toolbar-ui.module";
import { TOOLBAR_FEATURE_KEY, toolbarReducer } from "../state/toolbar.reducer";
import { StoreModule } from "@ngrx/store";

@NgModule({
    declarations: [ToolbarFeatureComponent],
    imports: [
        CommonModule,
        ToolbarUiModule,
        StoreModule.forFeature(TOOLBAR_FEATURE_KEY, toolbarReducer),
    ],
    exports: [ToolbarFeatureComponent]
})
export class ToolbarFeatureModule {}