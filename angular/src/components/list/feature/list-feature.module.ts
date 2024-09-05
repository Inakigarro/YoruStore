import { NgModule } from "@angular/core";
import { ListFeatureComponent } from "./list-feature/list-feature.component";
import { CommonModule } from "@angular/common";
import { ListUiModule } from "../ui/list-ui.module";

@NgModule({
    declarations: [ListFeatureComponent],
    imports: [
        CommonModule,
        ListUiModule,
    ],
    exports: [ListFeatureComponent]
})
export class ListFeatureModule {}