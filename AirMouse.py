import tty
import fcntl
import struct

def get_mouse_position():
    """
    Gets the X and Y coordinates of the mouse pointer in TTY mode.

    Returns:
        A tuple (x, y) represents the mouse coordinates.
    """

    fd = open('/dev/input/mouse', 'rb')
    fcntl.ioctl(fd, 0x80045703)  # Set the mouse mode to relative

    while True:
        data = fd.read(3)
        if len(data) == 3:
            x, y, z = struct.unpack('BBB', data)
            return x, y

if __name__ == '__main__':
    x, y = get_mouse_position()
    print(f'Mouse position: X={x}, Y={y}')
