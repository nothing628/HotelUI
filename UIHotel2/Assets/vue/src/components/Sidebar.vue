<template>
    <div id="sidebar" class="sidebar">
        <!-- begin sidebar scrollbar -->
        <div data-scrollbar="true" data-height="100%">
            <!-- begin sidebar user -->
            <ul class="nav">
                <li class="nav-profile">
                    <div class="image">
                        <a href="javascript:;"><img src="assets/img/user-13.jpg" alt="" /></a>
                    </div>
                    <div class="info">
                        Sean Ngu
                        <small>Front end developer</small>
                    </div>
                </li>
            </ul>
            <!-- end sidebar user -->
            <!-- begin sidebar nav -->
            <ul class="nav">
                <li class="nav-header">Navigation</li>
                <menu-parent
                    text="Dashboard"
                    is-active
                    icon="fa fa-laptop">
                    <menu-child text="Dashboard v1"></menu-child>
                    <menu-child text="Dashboard v2"></menu-child>
                </menu-parent>
                <menu-parent
                    text="Music"
                    icon="fa fa-music"
                    badge="10">
                    <menu-child text="Player" href="/foo"></menu-child>
                    <menu-child text="Playlist" href="/"></menu-child>
                    <menu-child text="Settings"></menu-child>
                </menu-parent>
                <menu-child icon="fa fa-laptop" text="Calendar"></menu-child>
                <!-- begin sidebar minify button -->
                <li><a href="javascript:;" class="sidebar-minify-btn" data-click="sidebar-minify"><i class="fa fa-angle-double-left"></i></a></li>
                <!-- end sidebar minify button -->
            </ul>
            <!-- end sidebar nav -->
        </div>
        <!-- end sidebar scrollbar -->
    </div>
</template>
<script>
    import MenuParent from './Menu/MenuParent.vue'
    import MenuChild from './Menu/MenuChild.vue'
    export default {
        components: {
            MenuParent,
            MenuChild,
        }
    }
</script>