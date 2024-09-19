import { NgModule } from "@angular/core";
import { CardFeatureModule } from "./feature/card-feature.module";

@NgModule({
    imports: [CardFeatureModule],
    exports: [CardFeatureModule]
})
export class AppCardModule {}