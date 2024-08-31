import { NgModule } from "@angular/core";
import { AppToolbarUiComponent } from "./toolbar.component";
import { CommonModule } from "@angular/common";
import { MatToolbarModule } from "@angular/material/toolbar";
import { AppButtonModule } from "@root/components/button/button.module";

@NgModule({
    declarations: [AppToolbarUiComponent],
    imports: [
        CommonModule,
        MatToolbarModule,
        AppButtonModule
    ],
    exports: [AppToolbarUiComponent]
})
export class ToolbarUiModule {}