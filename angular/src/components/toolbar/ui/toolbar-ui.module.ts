import { NgModule } from "@angular/core";
import { ToolbarUiComponent } from "./toolbar-ui/toolbar-ui.component";
import { CommonModule } from "@angular/common";
import { MatToolbarModule } from "@angular/material/toolbar";
import { AppButtonModule } from "@components/button/button.module";

@NgModule({
    declarations: [ToolbarUiComponent],
    imports: [
        CommonModule,
        MatToolbarModule,
        AppButtonModule
    ],
    exports: [ToolbarUiComponent]
})
export class ToolbarUiModule {}