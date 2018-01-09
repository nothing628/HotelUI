<template>
    <div></div>
</template>
<script>
    export default {
        data() {
            return {
                dataValue: {
                    show: false,
                }
            }
        },
        watch: {
            dataValue: {
                handler() {
                    this.updateValue()
                },
                deep: true
            },
            value: {
                handler() {
                    if ('show' in this.value)
                        this.dataValue.show = this.value.show
                },
                deep: true
            }
        },
        props: {
            value: { type: Object, default() { return {}; } }
        },
        methods: {
            updateValue() {
                // Emit the number value through the input event
                this.$emit('input', this.dataValue)
            }
        }
    }
</script>
