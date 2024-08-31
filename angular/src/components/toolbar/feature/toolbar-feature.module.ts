import { NgModule } from "@angular/core";
import { AppToolbarFeatureComponent } from "./toolbar-feature.component";
import { CommonModule } from "@angular/common";
import { ToolbarUiModule } from "../ui/toolbar-ui.module";
import { TOOLBAR_FEATURE_KEY, toolbarReducer } from "../state/toolbar.reducer";
import { StoreModule } from "@ngrx/store";

@NgModule({
    declarations: [AppToolbarFeatureComponent],
    imports: [
        CommonModule,
        ToolbarUiModule,
        StoreModule.forFeature(TOOLBAR_FEATURE_KEY, toolbarReducer),
    ],
    exports: [AppToolbarFeatureComponent]
})
export class ToolbarFeatureModule {}