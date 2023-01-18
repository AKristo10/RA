import random

from pyglet.gl import *
from pyglet.window import *

win_size_x = 1000
win_size_y = 800
window = pyglet.window.Window(win_size_x, win_size_y)


class Particle:
    def __init__(self, img, xpos, ypos, pbatch, pcolor):
        self.image = img
        self.duration = 10
        self.size_x = random.uniform(-2, 2)
        self.size_y = random.uniform(-2, 2)
        self.sprite = pyglet.sprite.Sprite(img=image, x=xpos, y=ypos, batch=pbatch)
        self.sprite.scale = 0.20
        self.sprite.color = pcolor

    def is_alive(self):
        return self.duration <= 0

    def update(self, dx, dy):
        self.sprite.x += self.size_x + dx
        self.sprite.y += self.size_y + dy
        # smanji velicinu da skuzis nestajanje
        self.sprite.scale *= 0.90
        # smanji trajanje cestice da se moze ugasiti
        self.duration -= 1


def update(interval):
    global x, y, particles, batch, color
    for p in particles:
        if p.is_alive():
            particles.remove(p)
        p.update(50, 50)

    particles.append(Particle(image, x, y, batch, color))


@window.event
def on_draw():
    window.clear()
    batch.draw()


@window.event
def on_key_press(symbol, modifiers):
    global x, y, color
    # implementacija pomaka gore, dolje, lijevo desno
    if symbol == key.UP:
        y += 10
    if symbol == key.DOWN:
        y -= 10
    if symbol == key.LEFT:
        x -= 10
    if symbol == key.RIGHT:
        x += 10

    # promjena boje
    if symbol == key.B:
        color = (0, 0, 255)  # blue
    if symbol == key.R:
        color = (255, 0, 0)  # red
    if symbol == key.W:
        color = (255, 255, 255)  # white
    if symbol == key.P:
        color = (255, 105, 180)  # pink


if __name__ == "__main__":
    x = 50
    y = 50
    batch = pyglet.graphics.Batch()
    particles = []
    # pocetna boja je bijela
    color = (255, 255, 255)
    image = pyglet.image.load("explosion.bmp")
    window.set_icon(image)
    pyglet.clock.schedule_interval(update, interval=0.005)
    pyglet.app.run()
