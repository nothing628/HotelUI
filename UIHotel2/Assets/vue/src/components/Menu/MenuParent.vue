<template>
    <li :class="active_class">
        <a @click="linkClicked" href="javascript:;">
            <b v-if="!is_badge" class="caret pull-right"></b>
            <span v-if="is_badge" class="badge pull-right">{{ badge }}</span>
            <i v-if="is_icon" :class="icon_class"></i>
            <span>{{ text }}</span>
        </a>
        <transition name="slide-fade">
            <ul class="sub-menu" v-show="is_expand">
                <slot></slot>
            </ul>
        </transition>
    </li>
</template>
<style>
    .has-sub.expand .sub-menu,
    .has-sub .sub-menu.slide-fade-leave-active {
        display: block;
    }

    .slide-fade-enter-active, .slide-fade-leave-active {
        transition: all .5s linear;
    }

    .slide-fade-enter, .slide-fade-leave-to {
        transform: translateX(-100%);
    }
</style>
<script>
    export default {
        computed: {
            style_submenu() {
                if (this.is_expand)
                    return {display: 'block'}
                return {display: 'none'}
            },
            active_class() {
                let list_class = ['has-sub']

                if (this.is_expand)
                    list_class.push('expand')

                if (this.isActive)
                    list_class.push('active')
                
                return list_class
            },
            icon_class() {
                let list_class = this.icon.split(' ')

                return list_class
            },
            is_icon() {
                return this.icon != ''
            },
            is_badge() {
                return this.badge != ''
            }
        },
        data() {
            return {
                is_expand: false,
            }
        },
        props: {
            isActive: { type: Boolean, required: false, default: false },
            text: { type: String, required: true },
            badge: { type: String, required: false, default: ''},
            href: { type: String, required: false, default: ''},
            icon: { type: String, required: false, default: ''}
        },
        methods: {
            linkClicked() {
                if (this.href != '') {
                    this.$emit('click', this.href)
                    this.$router.push(this.href)
                }
                
                this.is_expand = !this.is_expand
            }
        }
    }
</script>