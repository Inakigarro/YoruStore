import { NgModule } from "@angular/core";
import { ListUiComponent } from "./list-ui/list-ui.component";
import { CommonModule } from "@angular/common";
import { AppCardModule } from "@components/card/card.module";
import { MatGridListModule } from "@angular/material/grid-list";

@NgModule({
    declarations: [ListUiComponent],
    imports: [
        CommonModule,
        MatGridListModule,
        AppCardModule
    ],
    exports: [ListUiComponent]
})
export class ListUiModule {}