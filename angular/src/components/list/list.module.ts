import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MatListModule } from "@angular/material/list"
import { AppListUiComponent } from "./ui/list-ui.component";
import { AppCardModule } from "../card/card.module";

@NgModule({
    declarations: [AppListUiComponent],
    imports: [
        CommonModule,
        MatListModule,
        AppCardModule
    ],
    exports: [AppListUiComponent]
})
export class AppListModule {}