import { NgModule } from "@angular/core";
import { ListUiComponent } from "./list-ui/list-ui.component";
import { CommonModule } from "@angular/common";
import { AppCardModule } from "@components/card/card.module";
import { MatGridListModule } from "@angular/material/grid-list";
import { GridColsDirective } from "./grid-cols.directive";

@NgModule({
    declarations: [
        ListUiComponent,GridColsDirective,],
    imports: [
        CommonModule,
        MatGridListModule,
        AppCardModule
    ],
    exports: [ListUiComponent]
})
export class ListUiModule {}