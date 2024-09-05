import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import { CardUiModule } from "../ui/card-ui.module";
import { CardFeatureComponent } from "./card-feature/card-feature.component";

@NgModule({
    declarations: [CardFeatureComponent],
    imports: [
        CommonModule,
        CardUiModule,
        StoreModule.forFeature('card-feature', {}),
    ],
    exports: [CardFeatureComponent]
})
export class CardFeatureModule {}