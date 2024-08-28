import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MatToolbarModule } from "@angular/material/toolbar";
import { AppButtonModule } from "../button/button.module";
import { AppToolbarComponent } from "./ui/toolbar.component";
import { StoreModule } from "@ngrx/store";
import { TOOLBAR_FEATURE_KEY, toolbarReducer } from "./state/toolbar.reducer";
import { AppToolbarFeatureComponent } from "./feature/toolbar-feature.component";

@NgModule({
    declarations: [
        AppToolbarComponent,
        AppToolbarFeatureComponent],
    imports: [
        CommonModule,
        MatToolbarModule,
        AppButtonModule,
        StoreModule.forFeature(TOOLBAR_FEATURE_KEY, toolbarReducer),
    ],
    exports: [AppToolbarComponent, AppToolbarFeatureComponent]
})
export class AppToolbarModule {}