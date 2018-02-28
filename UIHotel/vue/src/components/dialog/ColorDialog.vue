<template>
    <v-dialog v-model="modal" scrollable max-width="600px">
        <v-card>
            <v-card-title><strong>Choose Color</strong></v-card-title>
            <v-divider></v-divider>
            <v-card-text>
                <table class="table table-color">
                    <tbody>
                        <tr v-for="colvar,colname in colors">
                            <td v-for="colhex,colvarname in colvar" @click="selectColor(colname, colvarname)" :style="createStyle(colhex)">&nbsp;</td>
                        </tr>
                    </tbody>
                </table>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="red darken-1" dark @click.native="cancelDialog">Cancel</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<style>
    table.table.table-color tbody td {
        padding: 0px;
        cursor: pointer;
        height: 19px;
    }
</style>
<script>
    import VColors from 'vuetify/es5/util/colors'
    import ChangeCase from 'change-case'
    
    export default {
        data() {
            return {
                modal: false,
                dataValue: null,
                colors: {},
            }
        },
        watch: {
            dataValue: {
                handler() {
                    this.updateValue()
                }
            },
            value: {
                handler() {
                    this.dataValue = this.value
                }
            },
            showModal: {
                handler() {
                    this.modal = this.showModal
                }
            }
        },
        props: {
            value: { type: String, default: null },
            showModal: { type: Boolean, default: false },
        },
        methods: {
            updateValue() {
                this.$emit('input', this.dataValue)
            },
            showDialog() {
                this.modal = true
            },
            cancelDialog() {
                this.modal = false
                this.$emit('cancel')
            },
            createStyle(hexcolor) {
                return { 'background-color': hexcolor }
            },
            normalizeVariantName(nameS) {
                if (nameS.startsWith('lighten'))
                    return nameS.replace('lighten', 'lighten-')
                else if (nameS.startsWith('darken'))
                    return nameS.replace('darken', 'darken-')
                else if (nameS.startsWith('accent'))
                    return nameS.replace('accent', 'accent-')
                else
                    return nameS
            },
            selectColor(colorPrimary, colorVariant) {
                let colP = ChangeCase.paramCase(colorPrimary)
                let colS = ChangeCase.paramCase(colorVariant)

                colS = this.normalizeVariantName(colS)

                if (colorPrimary == 'shades')
                    this.dataValue = colS
                else if (colorVariant == 'base')
                    this.dataValue = colP
                else
                    this.dataValue = colP + " " + colS

                this.$emit('save', this.dataValue)
                this.modal = false
            }
        },
        mounted() {
            this.$bus.$on('choose-color', this.showDialog)
            this.colors = VColors
        },
        beforeDestroy() {
            this.$bus.$off('choose-color', this.showDialog)
        }
    }
</script>
