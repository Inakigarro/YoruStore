import { NgModule } from "@angular/core";
import { ButtonComponent } from "./button.component";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatBadgeModule } from "@angular/material/badge";
import { ButtonFeatureComponent } from "./feature/button-feature.component";

@NgModule({
    declarations: [ButtonComponent, ButtonFeatureComponent],
    imports: [
        CommonModule,
        MatButtonModule,
        MatIconModule,
        MatBadgeModule,
    ],
    exports: [ButtonComponent, ButtonFeatureComponent]
})
export class AppButtonModule {}